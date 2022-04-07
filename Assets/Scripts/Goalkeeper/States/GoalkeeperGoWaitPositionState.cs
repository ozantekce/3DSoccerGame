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


        if (Ball.Instance.IsShoted())
        {

            float meetingTime = GoalkeeperCalculater.FindMeetingTime(
                goalkeeper.transform.position, Ball.Instance.transform.position, Ball.Instance.GetVelocity()
                );
            Vector3 vel
                = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper.transform.position,
                Ball.Instance.transform.position, Ball.Instance.GetVelocity());

            if (vel.y > 0 && meetingTime > 0)
            {
                if (Mathf.Abs(vel.x) < goalkeeper.JumpPowerX
                    && Mathf.Abs(vel.y) < goalkeeper.JumpPowerY
                    )
                {
                    //Debug.Log(goalkeeper.name+" "+meetingTime+" "+vel+" "+Ball.Instance.GetVelocity());
                    GoalkeeperJumpState.jumpVelocity = vel;
                    goalkeeper.ChangeCurrentState(GoalkeeperJumpState.goalkeeperJumpState);
                    return;
                }
                else
                {

                    GoalkeeperJumpState.jumpVelocity
                        = vel.normalized * goalkeeper.JumpPowerX;
                    goalkeeper.ChangeCurrentState(GoalkeeperJumpState.goalkeeperJumpState);
                    return;
                }
            }


        }


        if (0.5f > Vector3.Distance(goalkeeper.WaitPosition, goalkeeper.transform.position))
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
        if (0.5f > Vector3.Distance(goalkeeper.WaitPosition, goalkeeper.transform.position))
            goalkeeper.Rb.velocity = Vector3.zero;

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