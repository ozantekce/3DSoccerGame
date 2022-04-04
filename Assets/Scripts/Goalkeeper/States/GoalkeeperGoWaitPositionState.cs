using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperGoWaitPositionState : GoalkeeperState
{

    public static GoalkeeperGoWaitPositionState goalkeeperGoWaitPositionState = new GoalkeeperGoWaitPositionState();


    public void EnterTheState(Goalkeeper goalkeeper)
    {

        goalkeeper.ChangeAnimation("Run");

    }

    public void ExecuteTheState(Goalkeeper goalkeeper)
    {

        float meetingTime = FindMeetingTime(
            goalkeeper.transform.position, Ball.Instance.transform.position, Ball.Instance.GetVelocity()
            );


        Vector3 vel
            = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper.transform.position,
            Ball.Instance.transform.position, Ball.Instance.GetVelocity());


        if (vel.magnitude <= 5f || meetingTime < 0)
            ;
        else
        {
            if (Mathf.Abs(vel.x) <= goalkeeper.JumpPowerX
                && Mathf.Abs(vel.y) <= goalkeeper.JumpPowerY)
            {

                GoalkeeperJumpState.jumpVelocity = vel;
                goalkeeper.ChangeCurrentState(GoalkeeperJumpState.goalkeeperJumpState);
            }


        }


        if (3f > Vector3.Distance(goalkeeper.WaitPosition, goalkeeper.transform.position))
        {

            goalkeeper.ChangeCurrentState(GoalkeeperIdleState.goalkeeperIdleState);

        }
        else
        {
            MyMovePosition(goalkeeper, goalkeeper.WaitPosition, goalkeeper.MovementSpeed);
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
            speed
                = Mathf.Clamp(speed, 0, Vector3.Distance(position, goalkeeper.transform.position));
            goalkeeper.Rb.velocity = directionVector * speed;
        }


    }


    private float FindMeetingTime(Vector3 goalkeeperPosition, Vector3 ballPosition, Vector3 ballVelocity)
    {

        /*
         // daha iyi eksenlerden baðýmsýz ama yönetilmesi daha zor
         // ilerde üzerine düþünülebilir
        float angle = Vector3.Angle(goalkeeperPosition, ballPosition);

        float distance = Mathf.Cos(angle)* Vector3.Distance(goalkeeperPosition,ballPosition);

        float t = distance / ballVelocity.magnitude;

        return t;*/

        if (Mathf.Abs(ballVelocity.z) < 25f)
            return -1;

        return (goalkeeperPosition.z - ballPosition.z) / ballVelocity.z;
    }


}