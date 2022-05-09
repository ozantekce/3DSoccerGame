using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerIdleState : State
{

    private readonly static FootballerIdleState instance = new FootballerIdleState();
    public static FootballerIdleState Instance => instance;

    private FootballerIdleState()
    {

        //Debug.Log("FootballerIdleState created");


    }


    public override void Init()
    {

        AddTransition(new Transition(FoorballerFallState.Instance, ConditionMethods.FallCommand));

        AddTransition(new Transition(FootballerIdleWithBallState.Instance, ConditionMethods.ControlBall));

        AddTransition(new Transition(FootballerRunState.Instance, ConditionMethods.VerticalOrHorizontalInput));

        AddTransition(new Transition(FootballerSlideState.Instance, ConditionMethods.SlideInput));


        AddAction(new MyAction(FootballerActionMethods.SetAnimatorRunningParameterToFalse)
            , RunTimeOfAction.runOnEnter);

        AddAction(new MyAction(FootballerActionMethods.SetVelocityToZero));



    }




}
