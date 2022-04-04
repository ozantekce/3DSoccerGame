using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperIdleState : GoalkeeperState
{

    public static GoalkeeperIdleState goalkeeperIdleState = new GoalkeeperIdleState();

    public void EnterTheState(Goalkeeper goalkeeper)
    {
        goalkeeper.ChangeAnimation("IdleGoalkeeper");
    }

    public void ExecuteTheState(Goalkeeper goalkeeper)
    {


        float meetingTime = FindMeetingTime(
            goalkeeper.transform.position, Ball.Instance.transform.position, Ball.Instance.GetVelocity()
            );


        Vector3 vel
            = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper.transform.position,
            Ball.Instance.transform.position, Ball.Instance.GetVelocity());
        

        if (vel.magnitude <=5f || meetingTime < 0)
            return;

        if(Mathf.Abs(vel.x) <= goalkeeper.JumpPowerX
            && Mathf.Abs(vel.y)<= goalkeeper.JumpPowerY)
        {
            
            GoalkeeperJumpState.jumpVelocity = vel;
            goalkeeper.ChangeCurrentState(GoalkeeperJumpState.goalkeeperJumpState);
        }
        else
        {
            Debug.Log("vel : " + vel);
        }



    }

    public void ExitTheState(Goalkeeper goalkeeper)
    {
        
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
