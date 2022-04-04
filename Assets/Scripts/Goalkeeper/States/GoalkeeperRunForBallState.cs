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

}
