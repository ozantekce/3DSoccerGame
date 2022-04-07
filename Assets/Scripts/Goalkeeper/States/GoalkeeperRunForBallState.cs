using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperRunForBallState : GoalkeeperState
{

    public static GoalkeeperRunForBallState goalkeeperRunForBallState = new GoalkeeperRunForBallState();


    public void EnterTheState(Goalkeeper goalkeeper)
    {

        goalkeeper.ChangeAnimation("Run");

    }

    public void ExecuteTheState(Goalkeeper goalkeeper)
    {

        if (goalkeeper.BallVision.IsThereBallInVision())
        {
            goalkeeper.ChangeCurrentState(GoalkeeperShootState.goalkeeperShootState);
        }
        else if (19f < Vector3.Distance(goalkeeper.WaitPosition, goalkeeper.transform.position))
        {
            goalkeeper.ChangeCurrentState(GoalkeeperGoWaitPositionState.goalkeeperGoWaitPositionState);
        }
        else
        {
            Spin(goalkeeper);
            MyMovePosition(goalkeeper, Ball.Instance.transform.position, goalkeeper.MovementSpeed);
        }


    }

    public void ExitTheState(Goalkeeper goalkeeper)
    {

    }


    public void MyMovePosition(Goalkeeper goalkeeper, Vector3 position, float speed)
    {

        if (goalkeeper.transform.position != position)
        {
            Vector3 directionVector = position - goalkeeper.transform.position;
            directionVector = directionVector.normalized;
            directionVector.y = 0;
            goalkeeper.Rb.velocity = directionVector * speed;
        }


    }


    private float gap = 4f;        //  küçük açý farklarý görmezden gelinir
    private float spinSpeed = 500f; //  rotation deðiþim hýzý
    private void Spin(Goalkeeper goalkeeper)
    {
        Vector3 ballForwardVector = (goalkeeper.Ball.transform.position - goalkeeper.transform.position).normalized;
        Vector2 targetForward = new Vector3(ballForwardVector.x, ballForwardVector.z).normalized;


        Vector2 curretForward = new Vector2(goalkeeper.transform.forward.x, goalkeeper.transform.forward.z);

        //  aradaki açý bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki açý küçük deðil ise iþlem yapýlýr
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- dönme yönüne göre playerýn eulerAnglesý spinSpeed*Time.deltaTime kadar deðiþtirildi
            Vector3 eulerAng = goalkeeper.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1 : -1);
            goalkeeper.transform.eulerAngles = eulerAng;
            //---

        }

    }


}
