using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperIdleState : State
{


    private readonly static GoalkeeperIdleState instance = new GoalkeeperIdleState();
    public static GoalkeeperIdleState Instance => instance;
    private GoalkeeperIdleState()
    {
        //Debug.Log("GoalkeeperIdleState created");
    }

    public override void Init()
    {
        

        AddTransition(new Transition(GoalkeeperTakePositionState.Instance,ConditionMethods.GoalkeeperWrongPosition));
        AddTransition(new Transition(GoalkeeperGrabBallState.Instance, ConditionMethods.BallSoClose));
        AddTransition(new Transition(GoalkeeperJumpState.Instance, ConditionMethods.BallShotedAndGoalkeeperMeetingWithBall));


    }





}
