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
        

        if (vel.magnitude <=5f || meetingTime < 0 || !Ball.Instance.IsShoted())
        {
            //Debug.Log("vel : " + vel+" mt :"+meetingTime);
        }
        else
        {
            if (Mathf.Abs(vel.x) <= goalkeeper.JumpPowerX
                && Mathf.Abs(vel.y) <= goalkeeper.JumpPowerY)
            {

                GoalkeeperJumpState.jumpVelocity = vel;
                goalkeeper.ChangeCurrentState(GoalkeeperJumpState.goalkeeperJumpState);
                return;
            }


        }


        if (19f>Vector3.Distance(Ball.Instance.transform.position, goalkeeper.transform.position))
        {
            goalkeeper.ChangeCurrentState(GoalkeeperRunForBallState.goalkeeperRunForBallState);
        }
        else if (goalkeeper.BallVision.IsThereBallInVision())
        {
            goalkeeper.ChangeCurrentState(GoalkeeperShootState.goalkeeperShootState);
        }
        else if (2f < Vector3.Distance(goalkeeper.WaitPosition, goalkeeper.transform.position))
        {
            goalkeeper.ChangeCurrentState(GoalkeeperGoWaitPositionState.goalkeeperGoWaitPositionState);
        }

        Spin(goalkeeper);



    }

    public void ExitTheState(Goalkeeper goalkeeper)
    {
        
    }



    private float gap = 4f;        //  k���k a�� farklar� g�rmezden gelinir
    private float spinSpeed = 500f; //  rotation de�i�im h�z�
    private void Spin(Goalkeeper goalkeeper)
    {
        Vector3 ballForwardVector = (goalkeeper.Ball.transform.position - goalkeeper.transform.position).normalized;
        Vector2 targetForward = new Vector3(ballForwardVector.x, ballForwardVector.z).normalized;


        Vector2 curretForward = new Vector2(goalkeeper.transform.forward.x, goalkeeper.transform.forward.z);

        //  aradaki a�� bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki a�� k���k de�il ise i�lem yap�l�r
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- d�nme y�n�ne g�re player�n eulerAngles� spinSpeed*Time.deltaTime kadar de�i�tirildi
            Vector3 eulerAng = goalkeeper.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1 : -1);
            goalkeeper.transform.eulerAngles = eulerAng;
            //---

        }

    }


    private float FindMeetingTime(Vector3 goalkeeperPosition, Vector3 ballPosition, Vector3 ballVelocity)
    {

        float angle = Vector3.Angle(goalkeeperPosition, ballPosition);

        float distance = Mathf.Cos(angle)* Vector3.Distance(goalkeeperPosition,ballPosition);

        float t = distance / ballVelocity.magnitude;

        // daha iyi bir kontrol eklenmeli
        if (Mathf.Abs(ballVelocity.z) < 20f)
            return -1;

        return t;

    }


}
