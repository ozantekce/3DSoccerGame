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

        AddTransition(new Transition(FoorballerFallState.Instance, FootballerConditionMethods.FallCommand));

        AddTransition(new Transition(FootballerIdleState.Instance, FootballerConditionMethods.NoControlBall));

        AddTransition(new Transition(FootballerDribblingState.Instance, FootballerConditionMethods.VerticalOrHorizontalInput));

        AddTransition(new Transition(FootballerShotState.Instance, FootballerConditionMethods.ShotInput));

        AddTransition(new Transition(FootballerPassState.Instance, FootballerConditionMethods.PassInput));


        AddAction(new MyAction(FootballerActionMethods.SetAnimatorRunningParameterToFalse)
            , RunTimeOfAction.runOnEnter);

        AddAction(new MyAction(FootballerActionMethods.SetVelocityToZero));


    }




}

