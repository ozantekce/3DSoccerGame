using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperIdleWithBallState : State
{


    private readonly static GoalkeeperIdleWithBallState instance = new GoalkeeperIdleWithBallState();
    public static GoalkeeperIdleWithBallState Instance => instance;
    private GoalkeeperIdleWithBallState()
    {

    }

    public override void Init()
    {

        AddAction(new MyAction(ActionMethods.GoalkeeperHoldTheBall));

        AddTransition(new Transition(GoalkeeperThrowBallState.Instance,ConditionMethods.Elapsed4SecondInState));


    }




}
