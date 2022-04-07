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
                goalkeeper.transform.position, Ball.Instance.transform.position, Ball.Instance.GetVelocity()
                );
            Vector3 vel
                = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper.transform.position,
                Ball.Instance.transform.position, Ball.Instance.GetVelocity());

            if(vel.y > 0 && meetingTime>0)
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




}
