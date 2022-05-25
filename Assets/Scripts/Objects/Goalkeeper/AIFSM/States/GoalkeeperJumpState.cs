using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperJumpState : State
{

    private readonly static GoalkeeperJumpState instance = new GoalkeeperJumpState();
    public static GoalkeeperJumpState Instance => instance;
    private GoalkeeperJumpState()
    {



    }


    public override void Init()
    {

        //  TRANSITIONS
        //  #1
        AddTransition(new Transition(GoalkeeperCatchTheBallState.Instance
            , GoalkeeperConditionMethods.BallCatchable)
            , RunTimeOfTransition.runOnPreExecution);
        //  #2
        AddTransition(new Transition(GoalkeeperIdleState.Instance
            , GoalkeeperConditionMethods.Elapsed4Seconds));



        //  ACTIONS
        //  #1
        AddAction(new MyAction(GoalkeeperActionMethods.Jump)
            ,RunTimeOfAction.runOnEnter);


    }





}
