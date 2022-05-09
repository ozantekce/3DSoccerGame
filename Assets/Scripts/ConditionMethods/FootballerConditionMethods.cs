using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerConditionMethods
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



    public static bool Elapsed2Seconds(FiniteStateMachine fsm)
    {

        if (fsm.ElapsedTimeInCurrentState() > 2f)
            return true;


        return false;
    }

    public static bool Elapsed4Seconds(FiniteStateMachine fsm)
    {

        if (fsm.ElapsedTimeInCurrentState() > 4f)
            return true;


        return false;
    }

}

