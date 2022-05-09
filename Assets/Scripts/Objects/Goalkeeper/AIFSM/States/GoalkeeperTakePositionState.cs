using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperTakePositionState : State
{

    private readonly static GoalkeeperTakePositionState instance = new GoalkeeperTakePositionState();
    public static GoalkeeperTakePositionState Instance => instance;
    private GoalkeeperTakePositionState()
    {



    }

    public override void Init()
    {

        //  TRANSITIONS
        //  #1
        AddTransition(new Transition(GoalkeeperJumpState.Instance
            , GoalkeeperConditionMethods.BallShotedAndMeetingWithThis));
        //  #2
        AddTransition(new Transition(GoalkeeperIdleState.Instance
            , GoalkeeperConditionMethods.RightPosition));


        //  ACTIONS
        //  #1
        AddAction(new MyAction(GoalkeeperActionMethods.GoRightPosition));
        //  #2
        AddAction(new MyAction(GoalkeeperActionMethods.Stop)
            ,RunTimeOfAction.runOnExit);
        

    }




}
