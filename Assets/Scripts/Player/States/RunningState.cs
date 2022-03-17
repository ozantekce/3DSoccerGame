using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState: PlayerState
{

    public static RunningState runningState = new RunningState();

    private State currentState = State.withoutBall;
    private enum State
    {
        withBall, withoutBall
    }

    public void EnterTheState(Player player)
    {

        if (currentState == State.withoutBall)
        {
            //Debug.Log("enter : RunningState_withoutBall");
            player.ChangeAnimation("Run");
        }
        else
        {
            //Debug.Log("enter : RunningState_withBall");
            player.ChangeAnimation("Run");
        }


    }

    public void ExitTheState(Player player)
    {

        if (currentState == State.withoutBall)
        {
            //Debug.Log("exit : RunningState_withoutBall");
        }
        else
        {
            //Debug.Log("exit : RunningState_withBall");
            // top yavaþlatýlmalý

            // topun mevcut hýz vektörü bulundu
            Vector3 currentBallVelocity = player.Ball.Rb.velocity;
            // hedef vektör için mevcut hýz vektörüne eklenmesi gerekli olan vektör bulundu
            Vector3 addVelocityToBall = Vector3.zero - currentBallVelocity;

            // eðer eklenecek vektör sýnýrlamadan büyük ise büyüklüðü ayarlandý
            if (addVelocityToBall.magnitude > ballTargetVelocity)
            {
                addVelocityToBall = addVelocityToBall.normalized * ballTargetVelocity;

            }
            // eklenecek vektör topa eklendi
            currentBallVelocity += addVelocityToBall;
            player.Ball.Rb.velocity = currentBallVelocity;
            //eðer top çok yavaþ ise direk hýzý 0 a eþitlenir
            if (player.Ball.Rb.velocity.magnitude < 3f)
            {
                player.Ball.Rb.velocity = Vector3.zero;
                player.Ball.Rb.angularVelocity = Vector3.zero;
            }

        }

    }

    public void ExecuteTheState(Player player)
    {
        // Running için Runningwithball ve Runningwithoutball diye 2 state oluþturmak yerine 2 methoda böldüm
        if (player.BallVision.IsThereBallInVision())
        {
            if (currentState != State.withBall)
            {
                currentState = State.withBall;
                EnterTheState(player);
            }
            ExecuteWithBall(player);
        }
        else
        {
            if (currentState != State.withoutBall)
            {
                currentState = State.withoutBall;
                EnterTheState(player);
            }
            ExecuteWithoutBall(player);
        }

    }

    private void ExecuteWithBall(Player player)
    {

        if (player.FallBySlide)
        {
            // ayaðýna kayýldý 
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if(player.Inputter.GetButtonShootValue() != 0)
        {   //Shoot inputu var shootState e gider
            player.ChangeCurrentState(ShootState.shootState);
        }
        else if (player.Inputter.GetButtonPassValue() != 0)
        {   //Pass inputu var passState e gider
            player.ChangeCurrentState(PassState.passState);
        }
        else if (player.Inputter.GetJoyStickVerticalValue() == 0
            && player.Inputter.GetJoyStickHorizontalValue() == 0)
        {
            // input olmadýðý için IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }


        //RunningState in iþlemleri yapýlýr
        RunWithBall(player);


    }


    private void ExecuteWithoutBall(Player player)
    {

        // ayaðýna kayýldý 
        if (player.FallBySlide)
        {
            
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        //Slide inputu var SlideState e gider
        else if (player.Inputter.GetButtonSlideValue() != 0)
        {   
            player.ChangeCurrentState(SlideState.slideState);

        }
        else if (player.Inputter.GetJoyStickVerticalValue() == 0
            && player.Inputter.GetJoyStickHorizontalValue() == 0)
        {   
            // input olmadýðý için IdleState gider
            player.ChangeCurrentState(IdleState.idleState);
            
        }

        //RunningState in iþlemleri yapýlýr
        Run(player);




    }



    private float gap  = 2f;        //  küçük açý farklarý görmezden gelinir
    private float spinSpeed = 500f; //  rotation deðiþim hýzý
    private void Run(Player player)
    {

        float inputVertical = -player.Inputter.GetJoyStickVerticalValue();
        float inputHorizontal = player.Inputter.GetJoyStickHorizontalValue();

        // inputa göre x ve z eksenindeki hedef Forward vektörü bulundu 
        Vector2 targetForward = new Vector2(inputVertical, inputHorizontal).normalized;
        // Rotation ayarlanmasý için spin methoduna gidilir
        Spin(player, targetForward);



        // y olmadan birim forward vektörü bulundu
        Vector3 directionVector 
            = new Vector3(player.transform.forward.x, 0, player.transform.forward.z).normalized;

        directionVector *= player.MovementSpeed;    // hýz vektörü oluþturuldu
        
        directionVector.y = player.Rb.velocity.y;   // y'nin deðiþmesini istemiyorum

        player.Rb.velocity = directionVector;       // playerýn hýzý ayarlandý





    }


    private float ballTargetVelocity = 15.5f;

    private float trackingToBallSpeed = 13f;

    private float minDistanceWithBall = 1.8f;

    private float ballMaxAddVelocity = 5.2f;

    private float minDistanceToAddVelocityToBall = 2f; // minDistanceWithBall dan büyük olmalý


    private Vector3 lastBallForward;
    private void RunWithBall(Player player)
    {

        float inputVertical = -player.Inputter.GetJoyStickVerticalValueRaw();
        float inputHorizontal = player.Inputter.GetJoyStickHorizontalValueRaw();

        // topla player arasýndaki mesafe bulundu
        float distanceWithBall = Vector3.Distance(player.transform.position, player.Ball.transform.position);

        //-----top takip ediliyor

        //  takip etmek için gerekli birim vektör bulundu
        Vector3 ballForwardVector = (player.Ball.transform.position - player.transform.position).normalized;

        // topun konumuna rotation ayarlanýyor
        Spin(player, new Vector3(ballForwardVector.x, ballForwardVector.z).normalized);

        Vector3 forwardVectorWithoutY
            = new Vector3(player.transform.forward.x, 0, player.transform.forward.z).normalized;

        float trackingSpeed = trackingToBallSpeed;
        // takip hýzýnýn top ile aradaki mesafe ile linear olmasý saðlanýyor
        trackingSpeed *= (distanceWithBall - minDistanceWithBall) * 5;
        trackingSpeed = Mathf.Clamp(trackingSpeed, 0, trackingToBallSpeed);// limiti geçmesini istemiyoruz

        forwardVectorWithoutY *= trackingSpeed;           // hýz vektörü oluþturuldu
        forwardVectorWithoutY.y = player.Rb.velocity.y;   // y nin sabit kalmasýný istiyorum
        player.Rb.velocity = forwardVectorWithoutY;       // hýz ayarlandý

        //-----


        //----topa hýz veriliyor


        //targetDirection = DirectionHelper.FindDirection(inputVertical, inputHorizontal);
        Vector3 ballTargetForwardVector = new Vector3(inputVertical, 0, inputHorizontal).normalized;

        
        float angle = Vector3.SignedAngle(ballForwardVector, ballTargetForwardVector, Vector3.up);
        if (Mathf.Abs(angle) > 160f)
        {
            Debug.Log(((int)Vector3.SignedAngle(ballForwardVector, ballTargetForwardVector, Vector3.up)));
            ballTargetForwardVector = Quaternion.AngleAxis(-90, Vector3.up) * ballTargetForwardVector;
        }


        bool needChangeVelocity;
        // yön deðiþimi oldu ise oyun tepki vermeli akýcýlýk için
        bool changeDirection = Vector3.Angle(ballTargetForwardVector,lastBallForward) > 40f;
        // top hýz verecek kadar yakýn mý ?
        needChangeVelocity = (distanceWithBall <= minDistanceToAddVelocityToBall) || changeDirection;

        if (needChangeVelocity)
        {

            // top için hedef hýzýn vektörü bulundu 
            Vector3 targetBallVelocity
                = ballTargetVelocity * (changeDirection ? 0.8f:1) * ballTargetForwardVector;
            // topun mevcut hýz vektörü bulundu
            Vector3 currentBallVelocity = player.Ball.Rb.velocity;
            // hedef vektör için mevcut hýz vektörüne eklenmesi gerekli olan vektör bulundu
            Vector3 addVelocityToBall = targetBallVelocity - currentBallVelocity;
            // eðer eklenecek vektör sýnýrlamadan büyük ise büyüklüðü ayarlandý
            addVelocityToBall = Vector3.ClampMagnitude(addVelocityToBall, ballMaxAddVelocity);

            // eklenecek vektör topa eklendi
            currentBallVelocity += addVelocityToBall;
            player.Ball.Rb.velocity = currentBallVelocity;

            lastBallForward = ballTargetForwardVector;

        }


    }
    
    private void Spin(Player player, Vector2 targetForward)
    {
        //  dönme x ve z eksenine göre y de yapýlýyor bu yüzden 2 boyutlu bir iþlem
        //  kuþ bakýþý yapýlýyor olarak düþünülebilir

        // x ve z eksenindeki mevcut Forward vektörü bulundu
        Vector2 curretForward = new Vector2(player.transform.forward.x, player.transform.forward.z);

        //  aradaki açý bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki açý küçük deðil ise iþlem yapýlýr
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- dönme yönüne göre playerýn eulerAnglesý spinSpeed*Time.deltaTime kadar deðiþtirildi
            Vector3 eulerAng = player.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1:-1);
            player.transform.eulerAngles = eulerAng;
            //---

        }

    }


}

