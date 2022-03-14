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

    public void Enter(Player player)
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

    public void Exit(Player player)
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
            if (player.Ball.Rb.velocity.magnitude < 3f)
            {
                player.Ball.Rb.velocity = Vector3.zero;
                player.Ball.Rb.angularVelocity = Vector3.zero;
            }
        }

    }

    public void Execute(Player player)
    {
        // Running için Runningwithball ve Runningwithoutball diye 2 state oluþturmak yerine 2 methoda böldüm
        if (player.BallVision.IsThereBallInVision())
        {
            if (currentState != State.withBall)
            {
                currentState = State.withBall;
                Enter(player);
            }
            WithBall(player);
        }
        else
        {
            if (currentState != State.withoutBall)
            {
                currentState = State.withoutBall;
                Enter(player);
            }
            WithoutBall(player);
        }

    }

    private void WithBall(Player player)
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
        else if (player.Inputter.GetJoyStickVerticalValue() != 0
            || player.Inputter.GetJoyStickHorizontalValue() != 0)
        {
            //RunningState in iþlemleri yapýlýr
            RunWithBall(player);

        }
        else
        {
            // input olmadýðý için IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }


    }


    private void WithoutBall(Player player)
    {


        if (player.FallBySlide)
        {
            // ayaðýna kayýldý 
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if(player.Inputter.GetButtonSlideValue() != 0)
        {   //Slide inputu var SlideState e gider
            player.ChangeCurrentState(SlideState.slideState);

        }
        else if (player.Inputter.GetJoyStickVerticalValue() != 0
            || player.Inputter.GetJoyStickHorizontalValue() != 0)
        {   //RunningState in iþlemleri yapýlýr
            Run(player);
        }
        else
        {
            // input olmadýðý için IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }


    }



    private float gap  = 2f;        //  küçük açý farklarý görmezden gelinir
    private float spinSpeed = 500f; //  rotation deðiþim hýzý
    private void Run(Player player)
    {

        float inputVertical = -player.Inputter.GetJoyStickVerticalValue();
        float inputHorizontal = player.Inputter.GetJoyStickHorizontalValue();

        // inputa göre birim vektör oluþturuldu 
        Vector3 directionVector = new Vector3(inputVertical, 0, inputHorizontal).normalized;

        directionVector *= player.MovementSpeed;    // hýz vektörü oluþturuldu
        
        directionVector.y = player.Rb.velocity.y;   // y'nin deðiþmesini istemiyorum

        player.Rb.velocity = directionVector;       // playerýn hýzý ayarlandý


        // inputa göre x ve z eksenindeki hedef Forward vektörü bulundu 
        Vector2 targetForward = new Vector2(inputVertical, inputHorizontal).normalized;
        Spin(player, targetForward);


    }


    private float ballTargetVelocity = 15f;

    private float ballMaxAddVelocity = 1.2f;

    private float trackingToBallSpeed = 13f;

    private float minDistanceWithBall = 1.8f;

    private float minDistanceToAddVelocityToBall = 2f; // minDistanceWithBall dan büyük olmalý
    private void RunWithBall(Player player)
    {

        float inputVertical = -player.Inputter.GetJoyStickVerticalValueRaw();
        float inputHorizontal = player.Inputter.GetJoyStickHorizontalValueRaw();
        // topla player arasýndaki mesafe bulundu
        float distanceWithBall = Vector3.Distance(player.transform.position, player.Ball.transform.position);
        
        // top yeteri kadar yakýnsa hýz verilebilir
        if (distanceWithBall <= minDistanceToAddVelocityToBall)
        {
            // input var mý ?
            if ((inputVertical != 0 || inputHorizontal != 0))
            {
                // top için hedef hýzýn vektörü bulundu 
                Vector3 targetBallVelocity
                    = ballTargetVelocity * new Vector3(inputVertical, 0, inputHorizontal).normalized;
                // topun mevcut hýz vektörü bulundu
                Vector3 currentBallVelocity = player.Ball.Rb.velocity;
                // hedef vektör için mevcut hýz vektörüne eklenmesi gerekli olan vektör bulundu
                Vector3 addVelocityToBall = targetBallVelocity - currentBallVelocity;

                // eðer eklenecek vektör sýnýrlamadan büyük ise büyüklüðü ayarlandý
                if (addVelocityToBall.magnitude > ballMaxAddVelocity)
                {
                    addVelocityToBall = addVelocityToBall.normalized * ballMaxAddVelocity;

                }
                // eklenecek vektör topa eklendi
                currentBallVelocity += addVelocityToBall;
                player.Ball.Rb.velocity = currentBallVelocity;

            }

        }


        // top takip ediliyor

        //  takip etmek için gerekli birim vektör bulundu
        Vector3 directionVector = player.Ball.transform.position - player.transform.position;
        directionVector = directionVector.normalized;

        float trackingSpeed = trackingToBallSpeed;
        // takip hýzýnýn top ile aradaki mesafe ile linear olmasý saðlanýyor
        trackingSpeed *= (distanceWithBall - minDistanceWithBall) * 5;
        trackingSpeed = Mathf.Clamp(trackingSpeed, 0, trackingToBallSpeed);// limiti geçmesini istemiyoruz


        directionVector *= trackingSpeed;           // hýz vektörü oluþturuldu
        directionVector.y = player.Rb.velocity.y;   // y nin sabit kalmasýný istiyorum

        player.Rb.velocity = directionVector;       // hýz ayarlandý

        // topun konumuna göre x ve z eksenindeki hedef vektör bulundu
        Vector2 targetForward = new Vector2(directionVector.x, directionVector.z).normalized;

        Spin(player,targetForward);

    }




    private void Spin(Player player, Vector2 targetForward)
    {
        // x ve z eksenindeki mevcut Forward vektörü bulundu
        Vector2 curretForward = new Vector2(player.transform.forward.x, player.transform.forward.z);

        //  aradaki açý bulundu
        float angleBetweenVectors = Vector2.Angle(curretForward, targetForward);

        // aradaki açý küçük deðil ise iþlem yapýlýr
        if (angleBetweenVectors > gap)
        {
            // cross iþlemi ile dönme yönü bulundu
            float direction = Vector3.Cross(curretForward, targetForward).z;

            //-- dönme yönüne göre playerýn eulerAnglesý spinSpeed*Time.deltaTime kadar deðiþtirildi
            Vector3 eulerAng = player.transform.eulerAngles;
            if (direction > 0)
            {
                eulerAng.y -= spinSpeed * Time.deltaTime;
            }
            else
            {
                eulerAng.y += spinSpeed * Time.deltaTime;

            }
            player.transform.eulerAngles = eulerAng;
            //---

        }

    }


}

