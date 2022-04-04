using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperRunState : GoalkeeperState
{

    public static GoalkeeperRunState goalkeeperRunState = new GoalkeeperRunState();
    private State currentState = State.withoutBall;
    private enum State
    {
        withBall, withoutBall
    }

    public void EnterTheState(Goalkeeper goalkeeper)
    {

        if (currentState == State.withoutBall)
        {
            //Debug.Log("enter : RunningState_withoutBall");
            goalkeeper.ChangeAnimation("Run");
        }
        else
        {
            //Debug.Log("enter : RunningState_withBall");
            goalkeeper.ChangeAnimation("Run");
        }


    }

    public void ExitTheState(Goalkeeper goalkeeper)
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
            Vector3 currentBallVelocity = goalkeeper.Ball.Rb.velocity;
            // hedef vekt�r i�in mevcut h�z vekt�r�ne eklenmesi gerekli olan vekt�r bulundu
            Vector3 addVelocityToBall = Vector3.zero - currentBallVelocity;

            // e�er eklenecek vekt�r s�n�rlamadan b�y�k ise b�y�kl��� ayarland�
            if (addVelocityToBall.magnitude > ballTargetVelocity)
            {
                addVelocityToBall = addVelocityToBall.normalized * ballTargetVelocity;

            }
            // eklenecek vekt�r topa eklendi
            currentBallVelocity += addVelocityToBall;
            goalkeeper.Ball.Rb.velocity = currentBallVelocity;
            //e�er top �ok yava� ise direk h�z� 0 a e�itlenir
            if (goalkeeper.Ball.Rb.velocity.magnitude < 3f)
            {
                goalkeeper.Ball.Rb.velocity = Vector3.zero;
                goalkeeper.Ball.Rb.angularVelocity = Vector3.zero;
            }

        }

    }

    public void ExecuteTheState(Goalkeeper goalkeeper)
    {
        // Running i�in Runningwithball ve Runningwithoutball diye 2 state olu�turmak yerine 2 methoda b�ld�m
        if (goalkeeper.BallVision.IsThereBallInVision())
        {
            if (currentState != State.withBall)
            {
                currentState = State.withBall;
                EnterTheState(goalkeeper);
            }
            ExecuteWithBall(goalkeeper);
        }
        else
        {
            if (currentState != State.withoutBall)
            {
                currentState = State.withoutBall;
                EnterTheState(goalkeeper);
            }
            ExecuteWithoutBall(goalkeeper);
        }

    }

    private void ExecuteWithBall(Goalkeeper goalkeeper)
    {

        if (goalkeeper.Inputter.GetButtonShootValue() != 0&& goalkeeper.BallVision.IsThereBallInVision())
        {   //Shoot inputu var shootState e gider
            goalkeeper.ChangeCurrentState(GoalkeeperShootState.goalkeeperShootState);
        }
        else if (goalkeeper.Inputter.GetJoyStickVerticalValue() == 0
            && goalkeeper.Inputter.GetJoyStickHorizontalValue() == 0)
        {
            // input olmad��� i�in IdleState gider
            goalkeeper.ChangeCurrentState(GoalkeeperIdleState.goalkeeperIdleState);

        }

        //RunningState in i�lemleri yap�l�r
        RunWithBall(goalkeeper);


    }


    private void ExecuteWithoutBall(Goalkeeper goalkeeper)
    {

        if (goalkeeper.Inputter.GetButtonJumpValue() != 0)
        {   //Jump inputu var jumpState e gider
            goalkeeper.ChangeCurrentState(GoalkeeperJumpState.goalkeeperJumpState);
        }
        else if (goalkeeper.Inputter.GetJoyStickVerticalValue() == 0
            && goalkeeper.Inputter.GetJoyStickHorizontalValue() == 0)
        {
            // input olmad��� i�in IdleState gider
            goalkeeper.ChangeCurrentState(GoalkeeperIdleState.goalkeeperIdleState);

        }

        //RunningState in i�lemleri yap�l�r
        Run(goalkeeper);




    }



    private float gap = 2f;        //  k���k a�� farklar� g�rmezden gelinir
    private float spinSpeed = 500f; //  rotation de�i�im h�z�
    private void Run(Goalkeeper goalkeeper)
    {

        float inputVertical = -goalkeeper.Inputter.GetJoyStickVerticalValue();
        float inputHorizontal = goalkeeper.Inputter.GetJoyStickHorizontalValue();

        // inputa g�re x ve z eksenindeki hedef Forward vekt�r� bulundu 
        //Vector2 targetForward = new Vector2(inputVertical, inputHorizontal).normalized;
        // Rotation ayarlanmas� i�in spin methoduna gidilir
        //Spin(goalkeeper, targetForward);



        // y olmadan birim forward vekt�r� bulundu
        Vector3 directionVector
            = new Vector3(inputVertical, 0, inputHorizontal).normalized;

        directionVector *= goalkeeper.MovementSpeed;    // h�z vekt�r� olu�turuldu

        directionVector.y = goalkeeper.Rb.velocity.y;   // y'nin de�i�mesini istemiyorum

        goalkeeper.Rb.velocity = directionVector;       // goalkeeper�n h�z� ayarland�





    }


    private float ballTargetVelocity = 15.5f;

    private float trackingToBallSpeed = 13f;

    private float minDistanceWithBall = 1.8f;

    private float ballMaxAddVelocity = 5.2f;

    private float minDistanceToAddVelocityToBall = 2f; // minDistanceWithBall dan b�y�k olmal�


    private Vector3 lastBallForward;
    private void RunWithBall(Goalkeeper goalkeeper)
    {

        float inputVertical = -goalkeeper.Inputter.GetJoyStickVerticalValue();
        float inputHorizontal = goalkeeper.Inputter.GetJoyStickHorizontalValue();

        // topla goalkeeper aras�ndaki mesafe bulundu
        float distanceWithBall = Vector3.Distance(goalkeeper.transform.position, goalkeeper.Ball.transform.position);

        //-----top takip ediliyor

        //  takip etmek i�in gerekli birim vekt�r bulundu
        Vector3 ballForwardVector = (goalkeeper.Ball.transform.position - goalkeeper.transform.position).normalized;

        // topun konumuna rotation ayarlan�yor
        Spin(goalkeeper, new Vector3(ballForwardVector.x, ballForwardVector.z).normalized);

        Vector3 forwardVectorWithoutY
            = new Vector3(goalkeeper.transform.forward.x, 0, goalkeeper.transform.forward.z).normalized;

        float trackingSpeed = trackingToBallSpeed;
        // takip h�z�n�n top ile aradaki mesafe ile linear olmas� sa�lan�yor
        trackingSpeed *= (distanceWithBall - minDistanceWithBall) * 5;
        trackingSpeed = Mathf.Clamp(trackingSpeed, 0, trackingToBallSpeed);// limiti ge�mesini istemiyoruz

        forwardVectorWithoutY *= trackingSpeed;           // h�z vekt�r� olu�turuldu
        forwardVectorWithoutY.y = goalkeeper.Rb.velocity.y;   // y nin sabit kalmas�n� istiyorum
        goalkeeper.Rb.velocity = forwardVectorWithoutY;       // h�z ayarland�

        //-----


        //----topa h�z veriliyor


        //targetDirection = DirectionHelper.FindDirection(inputVertical, inputHorizontal);
        Vector3 ballTargetForwardVector = new Vector3(inputVertical, 0, inputHorizontal).normalized;


        float angle = Vector3.SignedAngle(ballForwardVector, ballTargetForwardVector, Vector3.up);
        if (Mathf.Abs(angle) > 160f)
        {
            //Debug.Log(((int)Vector3.SignedAngle(ballForwardVector, ballTargetForwardVector, Vector3.up)));
            ballTargetForwardVector = Quaternion.AngleAxis(-90, Vector3.up) * ballTargetForwardVector;
        }


        bool needChangeVelocity;
        // y�n de�i�imi oldu ise oyun tepki vermeli ak�c�l�k i�in
        bool changeDirection = Vector3.Angle(ballTargetForwardVector, lastBallForward) > 40f;
        // top h�z verecek kadar yak�n m� ?
        needChangeVelocity = (distanceWithBall <= minDistanceToAddVelocityToBall) || changeDirection;

        if (needChangeVelocity)
        {

            // top i�in hedef h�z�n vekt�r� bulundu 
            Vector3 targetBallVelocity
                = ballTargetVelocity * (changeDirection ? 0.8f : 1) * ballTargetForwardVector;
            // topun mevcut h�z vekt�r� bulundu
            Vector3 currentBallVelocity = goalkeeper.Ball.Rb.velocity;
            // hedef vekt�r i�in mevcut h�z vekt�r�ne eklenmesi gerekli olan vekt�r bulundu
            Vector3 addVelocityToBall = targetBallVelocity - currentBallVelocity;
            // e�er eklenecek vekt�r s�n�rlamadan b�y�k ise b�y�kl��� ayarland�
            addVelocityToBall = Vector3.ClampMagnitude(addVelocityToBall, ballMaxAddVelocity);

            // eklenecek vekt�r topa eklendi
            currentBallVelocity += addVelocityToBall;
            goalkeeper.Ball.Rb.velocity = currentBallVelocity;

            lastBallForward = ballTargetForwardVector;

        }


    }

    private void Spin(Goalkeeper goalkeeper, Vector2 targetForward)
    {

        //  d�nme x ve z eksenine g�re y de yap�l�yor bu y�zden 2 boyutlu bir i�lem
        //  ku� bak��� yap�l�yor olarak d���n�lebilir

        // x ve z eksenindeki mevcut Forward vekt�r� bulundu
        Vector2 curretForward = new Vector2(goalkeeper.transform.forward.x, goalkeeper.transform.forward.z);

        //  aradaki a�� bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki a�� k���k de�il ise i�lem yap�l�r
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- d�nme y�n�ne g�re goalkeeper�n eulerAngles� spinSpeed*Time.deltaTime kadar de�i�tirildi
            Vector3 eulerAng = goalkeeper.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1 : -1);
            goalkeeper.transform.eulerAngles = eulerAng;
            //---

        }

    }


}
