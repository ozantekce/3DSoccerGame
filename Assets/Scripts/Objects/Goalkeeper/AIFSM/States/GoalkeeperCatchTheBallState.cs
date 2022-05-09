using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperCatchTheBallState : State
{


    private readonly static GoalkeeperCatchTheBallState instance = new GoalkeeperCatchTheBallState();
    public static GoalkeeperCatchTheBallState Instance => instance;
    private GoalkeeperCatchTheBallState()
    {



    }
    
    public override void Init()
    {

        //  TRANSITIONS
        //  #1
        AddTransition(new Transition(GoalkeeperIdleWithBallState.Instance,
            GoalkeeperConditionMethods.BallIsTrigger));


        //  ACTIONS
        //  #1
        AddAction(new MyAction(GoalkeeperActionMethods.CatchBall)
            ,RunTimeOfAction.runOnEnter);



    }



}

