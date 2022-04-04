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


    }

    private void ExecuteWithBall(Goalkeeper goalkeeper)
    {



    }


    private void ExecuteWithoutBall(Goalkeeper goalkeeper)
    {





    }



    private float gap = 2f;        //  k���k a�� farklar� g�rmezden gelinir
    private float spinSpeed = 500f; //  rotation de�i�im h�z�
    private void Run(Goalkeeper goalkeeper)
    {




    }


    private float ballTargetVelocity = 15.5f;

    private float trackingToBallSpeed = 13f;

    private float minDistanceWithBall = 1.8f;

    private float ballMaxAddVelocity = 5.2f;

    private float minDistanceToAddVelocityToBall = 2f; // minDistanceWithBall dan b�y�k olmal�


    private Vector3 lastBallForward;
    private void RunWithBall(Goalkeeper goalkeeper)
    {


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
