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
            if (player.Ball.Rb.velocity.magnitude < 3f)
            {
                player.Ball.Rb.velocity = Vector3.zero;
                player.Ball.Rb.angularVelocity = Vector3.zero;
            }
        }

    }

    public void Execute(Player player)
    {
        // Running i�in Runningwithball ve Runningwithoutball diye 2 state olu�turmak yerine 2 methoda b�ld�m
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
            // aya��na kay�ld� 
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
            //RunningState in i�lemleri yap�l�r
            RunWithBall(player);

        }
        else
        {
            // input olmad��� i�in IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }


    }


    private void WithoutBall(Player player)
    {


        if (player.FallBySlide)
        {
            // aya��na kay�ld� 
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if(player.Inputter.GetButtonSlideValue() != 0)
        {   //Slide inputu var SlideState e gider
            player.ChangeCurrentState(SlideState.slideState);

        }
        else if (player.Inputter.GetJoyStickVerticalValue() != 0
            || player.Inputter.GetJoyStickHorizontalValue() != 0)
        {   //RunningState in i�lemleri yap�l�r
            Run(player);
        }
        else
        {
            // input olmad��� i�in IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }


    }



    private float gap  = 2f;        //  k���k a�� farklar� g�rmezden gelinir
    private float spinSpeed = 500f; //  rotation de�i�im h�z�
    private void Run(Player player)
    {

        float inputVertical = -player.Inputter.GetJoyStickVerticalValue();
        float inputHorizontal = player.Inputter.GetJoyStickHorizontalValue();

        // inputa g�re birim vekt�r olu�turuldu 
        Vector3 directionVector = new Vector3(inputVertical, 0, inputHorizontal).normalized;

        directionVector *= player.MovementSpeed;    // h�z vekt�r� olu�turuldu
        
        directionVector.y = player.Rb.velocity.y;   // y'nin de�i�mesini istemiyorum

        player.Rb.velocity = directionVector;       // player�n h�z� ayarland�


        // inputa g�re x ve z eksenindeki hedef Forward vekt�r� bulundu 
        Vector2 targetForward = new Vector2(inputVertical, inputHorizontal).normalized;
        Spin(player, targetForward);


    }


    private float ballTargetVelocity = 15f;

    private float ballMaxAddVelocity = 1.2f;

    private float trackingToBallSpeed = 13f;

    private float minDistanceWithBall = 1.8f;

    private float minDistanceToAddVelocityToBall = 2f; // minDistanceWithBall dan b�y�k olmal�
    private void RunWithBall(Player player)
    {

        float inputVertical = -player.Inputter.GetJoyStickVerticalValueRaw();
        float inputHorizontal = player.Inputter.GetJoyStickHorizontalValueRaw();
        // topla player aras�ndaki mesafe bulundu
        float distanceWithBall = Vector3.Distance(player.transform.position, player.Ball.transform.position);
        
        // top yeteri kadar yak�nsa h�z verilebilir
        if (distanceWithBall <= minDistanceToAddVelocityToBall)
        {
            // input var m� ?
            if ((inputVertical != 0 || inputHorizontal != 0))
            {
                // top i�in hedef h�z�n vekt�r� bulundu 
                Vector3 targetBallVelocity
                    = ballTargetVelocity * new Vector3(inputVertical, 0, inputHorizontal).normalized;
                // topun mevcut h�z vekt�r� bulundu
                Vector3 currentBallVelocity = player.Ball.Rb.velocity;
                // hedef vekt�r i�in mevcut h�z vekt�r�ne eklenmesi gerekli olan vekt�r bulundu
                Vector3 addVelocityToBall = targetBallVelocity - currentBallVelocity;

                // e�er eklenecek vekt�r s�n�rlamadan b�y�k ise b�y�kl��� ayarland�
                if (addVelocityToBall.magnitude > ballMaxAddVelocity)
                {
                    addVelocityToBall = addVelocityToBall.normalized * ballMaxAddVelocity;

                }
                // eklenecek vekt�r topa eklendi
                currentBallVelocity += addVelocityToBall;
                player.Ball.Rb.velocity = currentBallVelocity;

            }

        }


        // top takip ediliyor

        //  takip etmek i�in gerekli birim vekt�r bulundu
        Vector3 directionVector = player.Ball.transform.position - player.transform.position;
        directionVector = directionVector.normalized;

        float trackingSpeed = trackingToBallSpeed;
        // takip h�z�n�n top ile aradaki mesafe ile linear olmas� sa�lan�yor
        trackingSpeed *= (distanceWithBall - minDistanceWithBall) * 5;
        trackingSpeed = Mathf.Clamp(trackingSpeed, 0, trackingToBallSpeed);// limiti ge�mesini istemiyoruz


        directionVector *= trackingSpeed;           // h�z vekt�r� olu�turuldu
        directionVector.y = player.Rb.velocity.y;   // y nin sabit kalmas�n� istiyorum

        player.Rb.velocity = directionVector;       // h�z ayarland�

        // topun konumuna g�re x ve z eksenindeki hedef vekt�r bulundu
        Vector2 targetForward = new Vector2(directionVector.x, directionVector.z).normalized;

        Spin(player,targetForward);

    }




    private void Spin(Player player, Vector2 targetForward)
    {
        // x ve z eksenindeki mevcut Forward vekt�r� bulundu
        Vector2 curretForward = new Vector2(player.transform.forward.x, player.transform.forward.z);

        //  aradaki a�� bulundu
        float angleBetweenVectors = Vector2.Angle(curretForward, targetForward);

        // aradaki a�� k���k de�il ise i�lem yap�l�r
        if (angleBetweenVectors > gap)
        {
            // cross i�lemi ile d�nme y�n� bulundu
            float direction = Vector3.Cross(curretForward, targetForward).z;

            //-- d�nme y�n�ne g�re player�n eulerAngles� spinSpeed*Time.deltaTime kadar de�i�tirildi
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

