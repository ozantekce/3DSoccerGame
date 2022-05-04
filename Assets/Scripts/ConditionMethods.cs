using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionMethods
{



    public static bool VerticalOrHorizontalInput(FiniteStateMachine fsm)
    {
        Footballer footballer = fsm.GetComponent<Footballer>();

        return footballer.verticalInput != 0 || footballer.horizontalInput != 0;

    }

    public static bool NoVerticalOrHorizontalInput(FiniteStateMachine fsm)
    {
        Footballer footballer = fsm.GetComponent<Footballer>();

        return !(footballer.verticalInput != 0 || footballer.horizontalInput != 0);

    }

    public static bool ControlBall(FiniteStateMachine fsm) {

        Player player = fsm.GetComponent<Player>();
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
        Footballer footballer = fsm.GetComponent<Footballer>();

        return footballer.slideInput != 0;

    }


    public static bool ShotInput(FiniteStateMachine fsm)
    {
        Footballer footballer = fsm.GetComponent<Footballer>();
        return footballer.shotInput!=0;

    }

    public static bool PassInput(FiniteStateMachine fsm)
    {
        Footballer footballer = fsm.GetComponent<Footballer>();
        return footballer.passInput != 0;

    }



    public static bool Shooting(FiniteStateMachine fsm)
    {
        Footballer player = fsm.GetComponent<Footballer>();
        return player.IsShooting;
    }
    public static bool NoShooting(FiniteStateMachine fsm)
    {
        return !Shooting(fsm);
    }


    public static bool Passing(FiniteStateMachine fsm)
    {
        Footballer player = fsm.GetComponent<Footballer>();
        return player.IsPassing;
    }
    public static bool NoPassing(FiniteStateMachine fsm)
    {
        return !Passing(fsm);
    }

    public static bool Sliding(FiniteStateMachine fsm)
    {
        Footballer player = fsm.GetComponent<Footballer>();
        return player.IsSliding;
    }
    public static bool NoSliding(FiniteStateMachine fsm)
    {
        return !Sliding(fsm);
    }


    public static bool Falling(FiniteStateMachine fsm)
    {
        Footballer player = fsm.GetComponent<Footballer>();
        return player.IsFalling;
    }

    public static bool NoFalling(FiniteStateMachine fsm)
    {
        return !Falling(fsm);
    }


    public static bool FallCommand(FiniteStateMachine fsm)
    {
        Footballer player = fsm.GetComponent<Footballer>();
        return player.FallCommand;

    }


    public static bool GoalkeeperRightPosition(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = fsm.GetComponent<Goalkeeper>();
        return goalkeeper.RightPosition();

    }

    public static bool GoalkeeperWrongPosition(FiniteStateMachine fsm)
    {

        return !GoalkeeperRightPosition(fsm);

    }


    public static bool BallSoClose(FiniteStateMachine fsm)
    {
        Transform transform = fsm.GetComponent<Transform>();

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
        if (!BallShoted(fsm))
            return false;



        return Ball.Instance.IsShoted;

    }


    public static bool BallGrabbableAndBallNotGrabbed(FiniteStateMachine fsm)
    {

        return BallGrabbable(fsm)&& BallNotGrabbed(fsm);

    }

    public static bool BallNotGrabbed(FiniteStateMachine fsm)
    {

        return !Ball.Instance.Rb.isKinematic;

    }

    public static bool BallGrabbed(FiniteStateMachine fsm)
    {

        return Ball.Instance.Rb.isKinematic;

    }

    public static bool BallGrabbable(FiniteStateMachine fsm)
    {
        Transform transform = fsm.GetComponent<Transform>();

        float distance
            = Vector3.Distance(transform.position, Ball.Instance.transform.position);

        return distance < 1.3f;
    }


    public static bool GoalkeeperReadyToThrow(FiniteStateMachine fsm)
    {

        return false;
    
    }

}

