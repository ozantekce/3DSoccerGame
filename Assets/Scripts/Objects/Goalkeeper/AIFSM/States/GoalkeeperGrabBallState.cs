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


        //  TRANSITIONS
        //  #1
        AddTransition(new Transition(GoalkeeperIdleWithBallState.Instance
            , GoalkeeperConditionMethods.BallIsTrigger));


        //  ACTIONS
        //  #1
        AddAction(new MyAction(GoalkeeperActionMethods.GrabBall)
            ,RunTimeOfAction.runOnEnter);

        
        

    }


    


}
