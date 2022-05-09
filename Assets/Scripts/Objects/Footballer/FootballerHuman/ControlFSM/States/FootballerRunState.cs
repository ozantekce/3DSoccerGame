using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerRunState : State
{
    
    
    private readonly static FootballerRunState instance = new FootballerRunState();
    public static FootballerRunState Instance => instance;
    private FootballerRunState()
    {
        //Debug.Log("FootballerRunState created");
    }



    public override void Init()
    {

        AddTransition(new Transition(FoorballerFallState.Instance, ConditionMethods.FallCommand));

        AddTransition(new Transition(FootballerIdleState.Instance, ConditionMethods.NoVerticalOrHorizontalInput));

        AddTransition(new Transition(FootballerDribblingState.Instance, ConditionMethods.ControlBall));

        AddTransition(new Transition(FootballerSlideState.Instance, ConditionMethods.SlideInput));

        AddAction(new MyAction(FootballerActionMethods.RunMethod));


        AddAction(new MyAction(FootballerActionMethods.SetAnimatorRunningParameterToTrue)
            , RunTimeOfAction.runOnEnter);




    }



}
