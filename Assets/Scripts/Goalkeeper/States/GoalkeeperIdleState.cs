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

        if (Ball.Instance.IsShoted())
        {
            
            float meetingTime = GoalkeeperCalculater.FindMeetingTime(
                goalkeeper.handPositionWhileJumping.position, Ball.Instance.transform.position, Ball.Instance.GetVelocity()
                );
            Vector3 jumpVelocity
                = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper.handPositionWhileJumping.position,
                Ball.Instance.transform.position, Ball.Instance.GetVelocity());

            if(jumpVelocity.y > 0 && meetingTime>0)
            {
                GoJumpState(goalkeeper,jumpVelocity);
            }


        }
        else if (19f>Vector3.Distance(Ball.Instance.transform.position, goalkeeper.transform.position))
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


    public void GoJumpState(Goalkeeper goalkeeper,Vector3 jumpVelocity)
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
