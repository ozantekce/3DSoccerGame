using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperGoWaitPositionState : GoalkeeperState
{

    public static GoalkeeperGoWaitPositionState goalkeeperGoWaitPositionState = new GoalkeeperGoWaitPositionState();


    public override void EnterTheState(Goalkeeper goalkeeper)
    {

        goalkeeper.ChangeAnimation("JogBackward");

    }

    public override void ExecuteTheState(Goalkeeper goalkeeper)
    {


        if (Ball.Instance.IsShoted())
        {

            float meetingTime = GoalkeeperCalculater.FindMeetingTime(
                goalkeeper.cacthArea.position, Ball.Instance.transform.position, Ball.Instance.GetVelocity()
                );
            Vector3 jumpVelocity
                = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper.cacthArea.position,
                Ball.Instance.transform.position, Ball.Instance.GetVelocity());

            if (jumpVelocity.y > 0 && meetingTime > 0)
            {
                GoJumpState(goalkeeper, jumpVelocity);
            }


        }
        else if (0.5f > Vector3.Distance(goalkeeper.WaitPosition, goalkeeper.transform.position))
        {

            goalkeeper.ChangeCurrentState(GoalkeeperIdleState.goalkeeperIdleState);

        }
        else
        {
            MyMovePosition(goalkeeper, goalkeeper.WaitPosition, goalkeeper.MovementSpeed);
        }
        

    }


    public override void ExitTheState(Goalkeeper goalkeeper)
    {
        if (0.5f > Vector3.Distance(goalkeeper.WaitPosition, goalkeeper.transform.position))
            goalkeeper.Rb.velocity = Vector3.zero;

    }


    public void GoJumpState(Goalkeeper goalkeeper, Vector3 jumpVelocity)
    {
        if (Mathf.Abs(jumpVelocity.x) < goalkeeper.JumpPowerX
            && Mathf.Abs(jumpVelocity.y) < goalkeeper.JumpPowerY
            )
        {
            //Debug.Log(goalkeeper.name+" "+meetingTime+" "+vel+" "+Ball.Instance.GetVelocity());
            GoalkeeperJumpState.jumpVelocity = jumpVelocity;
        }
        else
        {
            GoalkeeperJumpState.jumpVelocity
                = jumpVelocity.normalized * goalkeeper.JumpPowerX;
        }
        goalkeeper.ChangeCurrentState(GoalkeeperJumpState.goalkeeperJumpState);
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