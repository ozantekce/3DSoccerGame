using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoalkeeperState
{
    public abstract void EnterTheState(Goalkeeper goalkeeper);
    public abstract void ExecuteTheState(Goalkeeper goalkeeper);

    public abstract void ExitTheState(Goalkeeper goalkeeper);


    public static bool BallShooted()
    {
        return Ball.Instance.IsShoted();
    }

    public static bool IsThereBallInVision(Goalkeeper goalkeeper)
    {
        return goalkeeper.BallVision.IsThereBallInVision();
    }


    public static bool DistanceBetweenGoalkeeperAndCenterFar(Goalkeeper goalkeeper)
    {
        return 16f < Vector3.Distance(goalkeeper.Center.position, goalkeeper.transform.position);
    }


    public static bool DistanceBetweenGoalkeeperAndCenterSoFar(Goalkeeper goalkeeper)
    {
        return 19f < Vector3.Distance(goalkeeper.Center.position, goalkeeper.transform.position);
    }

    public static bool ActionsOver(Goalkeeper goalkeeper)
    {
        return !goalkeeper.ActionsOver();
    }

    public static bool BallCaught(Goalkeeper goalkeeper)
    {
        return goalkeeper.leftHand.HasBall || goalkeeper.rightHand.HasBall;
    }

    public static bool BallIsClose(Goalkeeper goalkeeper)
    {
        return 19f > Vector3.Distance(Ball.Instance.transform.position, goalkeeper.transform.position);
    }



    public static void GoJumpState(Goalkeeper goalkeeper)
    {

        float meetingTime = GoalkeeperCalculater.FindMeetingTime(
            goalkeeper.cacthArea.position, Ball.Instance.transform.position, Ball.Instance.GetVelocity()
            );
        Vector3 jumpVelocity
            = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper.cacthArea.position,
            Ball.Instance.transform.position, Ball.Instance.GetVelocity());

        if (jumpVelocity == Vector3.zero)
            return;

        if (jumpVelocity.y < 0)
            jumpVelocity.y = 0;

        if (jumpVelocity.y >= 0 && meetingTime > 0)
        {
            if (Mathf.Abs(jumpVelocity.x) < goalkeeper.JumpPowerX
                && Mathf.Abs(jumpVelocity.z) < goalkeeper.JumpPowerX
                && Mathf.Abs(jumpVelocity.y) < goalkeeper.JumpPowerY
                )
            {
                GoalkeeperJumpState.jumpVelocity = jumpVelocity;
            }
            else
            {
                GoalkeeperJumpState.jumpVelocity
                    = jumpVelocity.normalized * goalkeeper.JumpPowerX;
            }
            goalkeeper.ChangeCurrentState(GoalkeeperJumpState.goalkeeperJumpState);

        }

    }

    public static void GoGetBallState(Goalkeeper goalkeeper)
    {
        goalkeeper.ChangeCurrentState(GoalkeeperGetBallState.goalkeeperGetBallState);
    }

    public static void GoIdleState(Goalkeeper goalkeeper)
    {
        goalkeeper.ChangeCurrentState(GoalkeeperIdleState.goalkeeperIdleState);
    }

    /*
    public static void GoGoWaitPositionState(Goalkeeper goalkeeper)
    {
        goalkeeper.ChangeCurrentState(GoalkeeperGoWaitPositionState.goalkeeperGoWaitPositionState);
    }
    */

    public static void MyMovePosition(Goalkeeper goalkeeper, Vector3 position, float speed)
    {
        
        if (goalkeeper.transform.position != position)
        {
            //goalkeeper.ChangeAnimation("Run");
            Vector3 directionVector = position - goalkeeper.transform.position;
            directionVector = directionVector.normalized;
            directionVector.y = 0;
            goalkeeper.Rb.velocity = directionVector * speed;
        }
        else
        {
            //goalkeeper.ChangeAnimation("Idle");
        }


    }


    public static float gap = 4f;        //  k���k a�� farklar� g�rmezden gelinir
    public static float spinSpeed = 500f; //  rotation de�i�im h�z�
    public static void Spin(Goalkeeper goalkeeper)
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
