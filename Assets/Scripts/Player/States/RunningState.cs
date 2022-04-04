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
        player.ChangeAnimation("Run");

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
            // top yava�lat�lmal�

            // topun mevcut h�z vekt�r� bulundu
            Vector3 currentBallVelocity = player.Ball.Rb.velocity;
            // hedef vekt�r i�in mevcut h�z vekt�r�ne eklenmesi gerekli olan vekt�r bulundu
            Vector3 addVelocityToBall = Vector3.zero - currentBallVelocity;

            // e�er eklenecek vekt�r s�n�rlamadan b�y�k ise b�y�kl��� ayarland�
            if (addVelocityToBall.magnitude > ballTargetVelocity)
            {
                addVelocityToBall = addVelocityToBall.normalized * ballTargetVelocity;

            }
            // eklenecek vekt�r topa eklendi
            currentBallVelocity += addVelocityToBall;
            player.Ball.Rb.velocity = currentBallVelocity;
            //e�er top �ok yava� ise direk h�z� 0 a e�itlenir
            if (player.Ball.Rb.velocity.magnitude < 3f)
            {
                player.Ball.Rb.velocity = Vector3.zero;
                player.Ball.Rb.angularVelocity = Vector3.zero;
            }

        }

    }

    public void ExecuteTheState(Player player)
    {
        // Running i�in Runningwithball ve Runningwithoutball diye 2 state olu�turmak yerine 2 methoda b�ld�m
        if (player.BallVision.IsThereBallInVision())
        {
            currentState = State.withBall;
            ExecuteWithBall(player);
        }
        else
        {
            currentState = State.withoutBall;
            ExecuteWithoutBall(player);
        }
    }

    private void ExecuteWithBall(Player player)
    {
        if (player.FallBySlide)
        {
            // aya��na kay�ld� 
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if(player.ShootInput != 0)
        {   //Shoot inputu var shootState e gider
            player.ChangeCurrentState(ShootState.shootState);
        }
        else if (player.PassInput != 0)
        {   //Pass inputu var passState e gider
            player.ChangeCurrentState(PassState.passState);
        }
        else if (player.VerticalInput == 0
            && player.HorizontalInput == 0)
        {
            // input olmad��� i�in IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }


        //RunningState in i�lemleri yap�l�r
        RunWithBall(player);


    }


    private void ExecuteWithoutBall(Player player)
    {

        // aya��na kay�ld� 
        if (player.FallBySlide)
        {
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        //Slide inputu var SlideState e gider
        else if (player.SlideInput != 0)
        {   
            player.ChangeCurrentState(SlideState.slideState);

        }
        else if (player.VerticalInput== 0
            && player.HorizontalInput == 0)
        {   
            // input olmad��� i�in IdleState gider
            player.ChangeCurrentState(IdleState.idleState);
            
        }

        //RunningState in i�lemleri yap�l�r
        Run(player);




    }



    private float gap  = 4f;        //  k���k a�� farklar� g�rmezden gelinir
    private float spinSpeed = 500f; //  rotation de�i�im h�z�
    private void Run(Player player)
    {

        // inputa g�re x ve z eksenindeki hedef Forward vekt�r� bulundu 
        // verilen inputa g�re forward belirleniyor
        // mevcut forward ile hedeflenen forward ayn� olana kadar 
        // karakter d�nd�r�l�yor
        Vector2 targetForward 
            = new Vector2(player.VerticalInput, player.HorizontalInput).normalized;
        // Rotation ayarlanmas� i�in spin methoduna gidilir
        Spin(player, targetForward);

        // y olmadan birim forward vekt�r� bulundu
        Vector3 directionVector 
            = new Vector3(player.transform.forward.x, 0, player.transform.forward.z).normalized;

        directionVector *= player.MovementSpeed;    // h�z vekt�r� olu�turuldu
        
        directionVector.y = player.Rb.velocity.y;   // y'nin de�i�mesini istemiyorum

        player.Rb.velocity = directionVector;       // player�n h�z� ayarland�

    }


    private float ballTargetVelocity = 20f;

    private float trackingToBallSpeed = 15f;

    private float minDistanceWithBall = 2f;

    private float ballMaxAddVelocity = 4.2f;

    private float minDistanceToAddVelocityToBall = 2.5f; // minDistanceWithBall dan b�y�k olmal�


    private Vector3 lastBallForward;


    private void RunWithBall(Player player)
    {

        float inputVertical = player.VerticalInput;
        float inputHorizontal = player.HorizontalInput;

        // topla player aras�ndaki mesafe bulundu
        float distanceWithBall = Vector3.Distance(player.transform.position, player.Ball.transform.position);

        //-----top takip ediliyor

        //  takip etmek i�in gerekli birim vekt�r bulundu
        Vector3 ballForwardVector = (player.Ball.transform.position - player.transform.position).normalized;

        // topa do�ru rotation ayarlan�yor
        Spin(player, new Vector3(ballForwardVector.x, ballForwardVector.z).normalized);

        Vector3 forwardVectorWithoutY
            = new Vector3(player.transform.forward.x, 0, player.transform.forward.z).normalized;

        float trackingSpeed = trackingToBallSpeed;
        // takip h�z�n�n top ile aradaki mesafe ile linear olmas� sa�lan�yor
        trackingSpeed *= (distanceWithBall - minDistanceWithBall) * 5;
        trackingSpeed = Mathf.Clamp(trackingSpeed, 0, trackingToBallSpeed);// limiti ge�mesini istemiyoruz

        forwardVectorWithoutY *= trackingSpeed;           // h�z vekt�r� olu�turuldu
        forwardVectorWithoutY.y = player.Rb.velocity.y;   // y nin sabit kalmas�n� istiyorum
        player.Rb.velocity = forwardVectorWithoutY;       // h�z ayarland�

        //-----


        //----topa h�z veriliyor

        Vector3 ballTargetForwardVector
            = new Vector3(inputVertical, 0, inputHorizontal).normalized;


        float angle = Vector3.SignedAngle(ballForwardVector, ballTargetForwardVector, Vector3.up);
        if (Mathf.Abs(angle) > 160f)
        {
            //  targetForward ile mevcut forward aras�ndaki a�� fark�
            //  160 dereceden fazla ise targetForward mevcut forward�n 90 derece
            //  d�nd�r�lm�� halidir
            //  bunun sebebi -> y�n�nde ilerelerken <- de input geldi�inde
            //  topu �nce a�a�� y�n�ne sonra <- y�n�ne hareket ettirmek
            //  bu sayede top karaktere �arpmaz
            ballTargetForwardVector = Quaternion.AngleAxis(-90, Vector3.up) * ballTargetForwardVector;
        }


        bool needChangeVelocity;
        // y�n de�i�imi oldu mu ?
        // oldu ise top uzakta olsa bile h�z� de�i�meli
        bool changeDirection = Vector3.Angle(ballTargetForwardVector, lastBallForward) > 20f;

        // top h�z verecek kadar yak�n m� ?
        needChangeVelocity = (distanceWithBall <= minDistanceToAddVelocityToBall) || changeDirection;

        if (needChangeVelocity)
        {

            // top i�in hedef h�z�n vekt�r� bulundu
            // oyuncu ayn� y�ne do�ru gitmiyor ise top yak�n olmasa bile tepki g�sterir
            // ama hedef h�z 0.8f ile �arp�larak k���lt�l�r
      
            Vector3 targetBallVelocity
                = ballTargetVelocity * (changeDirection ? 0.8f : 1) * ballTargetForwardVector;
            // topun mevcut h�z vekt�r� bulundu
            Vector3 currentBallVelocity = player.Ball.Rb.velocity;
            // hedef vekt�r i�in mevcut h�z vekt�r�ne eklenmesi gerekli olan vekt�r bulundu
            Vector3 addVelocityToBall = targetBallVelocity - currentBallVelocity;
            // e�er eklenecek vekt�r s�n�rlamadan b�y�k ise b�y�kl��� ayarland�
            addVelocityToBall = Vector3.ClampMagnitude(addVelocityToBall, ballMaxAddVelocity);

            // eklenecek vekt�r topa eklendi
            currentBallVelocity += addVelocityToBall;
            player.Ball.Rb.velocity = currentBallVelocity;


            lastBallForward = ballTargetForwardVector;

        }



    }



    private void Spin(Player player, Vector2 targetForward)
    {
        //  d�nme x ve z eksenine g�re y de yap�l�yor bu y�zden 2 boyutlu bir i�lem
        //  ku� bak��� yap�l�yor olarak d���n�lebilir

        // x ve z eksenindeki mevcut Forward vekt�r� bulundu
        Vector2 curretForward = new Vector2(player.transform.forward.x, player.transform.forward.z);

        //  aradaki a�� bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki a�� k���k de�il ise i�lem yap�l�r
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- d�nme y�n�ne g�re player�n eulerAngles� spinSpeed*Time.deltaTime kadar de�i�tirildi
            Vector3 eulerAng = player.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1:-1);
            player.transform.eulerAngles = eulerAng;
            //---

        }

    }


}

