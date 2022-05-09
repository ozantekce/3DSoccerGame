using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperIdleState : State
{


    private readonly static GoalkeeperIdleState instance = new GoalkeeperIdleState();
    public static GoalkeeperIdleState Instance => instance;
    private GoalkeeperIdleState()
    {

    }

    public override void Init()
    {
        //  TRANSITIONS
        //  #1
        AddTransition(new Transition(GoalkeeperJumpState.Instance,
            GoalkeeperConditionMethods.BallShotedAndMeetingWithThis));
        //  #2
        AddTransition(new Transition(GoalkeeperGoTowardsTheBallState.Instance,
            GoalkeeperConditionMethods.BallSoClose));
        //  #3
        AddTransition(new Transition(GoalkeeperTakePositionState.Instance,
            GoalkeeperConditionMethods.WrongPosition));


        //  ACTIONS
        //  #1
        AddAction(new MyAction(GoalkeeperActionMethods.Stop));
        //  #2
        AddAction(new MyAction(GoalkeeperActionMethods.LookTheBall));

    }





}
