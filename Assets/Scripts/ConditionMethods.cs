using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionMethods
{



    public static bool VerticalOrHorizontalInput(FiniteStateMachine fsm)
    {
        Footballer footballer = ((FootballerFSM)fsm).Footballer;

        return footballer.verticalInput != 0 || footballer.horizontalInput != 0;

    }

    public static bool NoVerticalOrHorizontalInput(FiniteStateMachine fsm)
    {
        Footballer footballer = ((FootballerFSM)fsm).Footballer;

        return !(footballer.verticalInput != 0 || footballer.horizontalInput != 0);

    }

    public static bool ControlBall(FiniteStateMachine fsm) {

        Player player = ((PlayerFSM)fsm).Player;
        return player.BallVision.ControlBall();

    }

    public static bool NoControlBall(FiniteStateMachine fsm)
    {

        return !ControlBall(fsm);


    }

    public static bool ControlBallAndVerticalOrHorizontalInput(FiniteStateMachine fsm)
    {
        return ControlBall(fsm) && VerticalOrHorizontalInput(fsm);
    }

    public static bool ControlBallAndNoVerticalOrHorizontalInput(FiniteStateMachine fsm)
    {
        return ControlBall(fsm) && NoVerticalOrHorizontalInput(fsm);
    }


    public static bool NoControlBallAndNoVerticalOrHorizontalInput(FiniteStateMachine fsm)
    {

        return NoControlBall(fsm) && NoVerticalOrHorizontalInput(fsm);

    }

    public static bool NoControlBallAndVerticalOrHorizontalInput(FiniteStateMachine fsm)
    {

        return NoControlBall(fsm) && VerticalOrHorizontalInput(fsm);

    }


    public static bool NoControlBallAndSlideInput(FiniteStateMachine fsm)
    {

        return NoControlBall(fsm) && SlideInput(fsm);

    }

    public static bool SlideInput(FiniteStateMachine fsm)
    {
        Footballer footballer = ((FootballerFSM)fsm).Footballer;

        return footballer.slideInput != 0;

    }


    public static bool ShotInput(FiniteStateMachine fsm)
    {
        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        return footballer.shotInput!=0;

    }

    public static bool PassInput(FiniteStateMachine fsm)
    {
        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        return footballer.passInput != 0;

    }



    public static bool Shooting(FiniteStateMachine fsm)
    {
        Player player = ((PlayerFSM)fsm).Player;
        return player.IsShooting;
    }
    public static bool NoShooting(FiniteStateMachine fsm)
    {
        return !Shooting(fsm);
    }


    public static bool Passing(FiniteStateMachine fsm)
    {
        Player player = ((PlayerFSM)fsm).Player;
        return player.IsPassing;
    }
    public static bool NoPassing(FiniteStateMachine fsm)
    {
        return !Passing(fsm);
    }

    public static bool Sliding(FiniteStateMachine fsm)
    {
        Player player = ((PlayerFSM)fsm).Player;
        return player.IsSliding;
    }
    public static bool NoSliding(FiniteStateMachine fsm)
    {
        return !Sliding(fsm);
    }


    public static bool Falling(FiniteStateMachine fsm)
    {
        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        return footballer.IsFalling;
    }

    public static bool NoFalling(FiniteStateMachine fsm)
    {
        return !Falling(fsm);
    }


    public static bool FallCommand(FiniteStateMachine fsm)
    {
        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        return footballer.FallCommand;

    }


    public static bool GoalkeeperRightPosition(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        return goalkeeper.RightPosition();

    }

    public static bool GoalkeeperWrongPosition(FiniteStateMachine fsm)
    {

        return !GoalkeeperRightPosition(fsm);

    }


    public static bool BallSoClose(FiniteStateMachine fsm)
    {
        Transform transform = fsm.transform;

        float distance 
            = Vector3.Distance(transform.position,Ball.Instance.transform.position);

        return distance<10f;

    }

    public static bool NoBallSoClose(FiniteStateMachine fsm)
    {

        return !BallSoClose(fsm);

    }


    public static bool BallShoted(FiniteStateMachine fsm)
    {
        return Ball.Instance.IsShoted;
    }
    public static bool NoBallShoted(FiniteStateMachine fsm)
    {
        return !BallShoted(fsm);
    }


    public static bool GoalkeeperMeetingWithBall(FiniteStateMachine fsm)
    {

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Vector3 meetingPosition
            = GoalkeeperCalculater.FindMeetingPosition(goalkeeper, Ball.Instance.transform.position, Ball.Instance.Rb.velocity);


        Vector3 vel = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper, Ball.Instance.transform.position, Ball.Instance.Rb.velocity);

        /*
        if (Mathf.Abs(vel.x) > goalkeeper.JumpPowerX)
        {
            Debug.Log("vel :"+vel+" x : "+goalkeeper.JumpPowerX);
            return false;
        }

        if (vel.y > goalkeeper.JumpPowerY||vel.y<0)
        {
            Debug.Log("vel :" + vel + " x : " + goalkeeper.JumpPowerY);
            return false;
        }
            
        */


        return goalkeeper.Other.IntersectWithMeetingPosition(meetingPosition);

    }

    public static bool BallShotedAndGoalkeeperMeetingWithBall(FiniteStateMachine fsm)
    {

        return BallShoted(fsm)&&GoalkeeperMeetingWithBall(fsm);

    }


    public static bool BallGrabbableAndBallNotGrabbed(FiniteStateMachine fsm)
    {

        return BallGrabbable(fsm)&& NoBallGrabbed(fsm);

    }

    public static bool NoBallGrabbed(FiniteStateMachine fsm)
    {

        return !Ball.Instance.Rb.isKinematic;

    }

    public static bool BallGrabbed(FiniteStateMachine fsm)
    {

        return Ball.Instance.Rb.isKinematic;

    }

    public static bool BallGrabbable(FiniteStateMachine fsm)
    {
        Transform transform = fsm.transform;

        float distance
            = Vector3.Distance(transform.position, Ball.Instance.transform.position);

        return distance < 1.3f;
    }




    public static bool GoalkeeperBallCatchable(FiniteStateMachine fsm)
    {

        if (Ball.Instance.Rb.isKinematic)
            return false;

        Collider[] intersecting = Physics.OverlapSphere(Ball.Instance.transform.position, 3f);

        GameObject catchArea = ((GoalkeeperFSM)fsm).Goalkeeper.CatchArea;

        foreach (Collider c in intersecting)
        {
            if (c.gameObject == catchArea)
            {
                return true;
            }
        }

        return false;
    }


    public static bool BallCatched(FiniteStateMachine fsm)
    {

        return Ball.Instance.Rb.isKinematic;

    }


    public static bool Elapsed2SecondInState(FiniteStateMachine fsm)
    {

        if (fsm.ElapsedTimeInCurrentState() > 2f)
            return true;


        return false;
    }

    public static bool Elapsed4SecondInState(FiniteStateMachine fsm)
    {

        if (fsm.ElapsedTimeInCurrentState() > 4f)
            return true;


        return false;
    }

}

