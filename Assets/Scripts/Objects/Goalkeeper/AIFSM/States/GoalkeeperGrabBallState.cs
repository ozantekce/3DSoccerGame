using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperGrabBallState : State
{

    private readonly static GoalkeeperGrabBallState instance = new GoalkeeperGrabBallState();
    public static GoalkeeperGrabBallState Instance => instance;
    private GoalkeeperGrabBallState()
    {


    }

    public override void Init()
    {

        AddAction(new MyAction(GoalkeeperActionMethods.GrabBall),RunTimeOfAction.runOnEnter);

        AddTransition(new Transition(GoalkeeperIdleWithBallState.Instance, ConditionMethods.BallGrabbed));
        

    }




}
