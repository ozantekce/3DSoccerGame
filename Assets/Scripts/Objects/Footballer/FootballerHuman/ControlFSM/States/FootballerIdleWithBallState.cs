using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerIdleWithBallState : State
{

    private readonly static FootballerIdleWithBallState instance = new FootballerIdleWithBallState();
    public static FootballerIdleWithBallState Instance => instance;

    private FootballerIdleWithBallState()
    {

        //Debug.Log("FootballerIdleWithBallState created");


    }


    public override void Init()
    {

        AddTransition(new Transition(FoorballerFallState.Instance, ConditionMethods.FallCommand));

        AddTransition(new Transition(FootballerIdleState.Instance, ConditionMethods.NoControlBall));

        AddTransition(new Transition(FootballerDribblingState.Instance, ConditionMethods.VerticalOrHorizontalInput));

        AddTransition(new Transition(FootballerShotState.Instance, ConditionMethods.ShotInput));

        AddTransition(new Transition(FootballerPassState.Instance, ConditionMethods.PassInput));


        AddAction(new MyAction(FootballerActionMethods.SetAnimatorRunningParameterToFalse)
            , RunTimeOfAction.runOnEnter);

        AddAction(new MyAction(FootballerActionMethods.SetVelocityToZero));


    }




}

