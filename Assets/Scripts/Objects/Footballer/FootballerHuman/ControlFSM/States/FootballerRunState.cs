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
        AddTransition(new Transition(FootballerDribblingState.Instance, ConditionMethods.ControlBallAndVerticalOrHorizontalInput));
        AddTransition(new Transition(FootballerSlideState.Instance, ConditionMethods.NoControlBallAndSlideInput));

        AddAction(new MyAction(ActionMethods.FootballerRunMethod));


        AddAction(new MyAction(ActionMethods.SetAnimatorRunningParameterToTrue), RunTimeOfAction.runOnEnter);


        AddAction(new MyAction((FiniteStateMachine fsm) => {
            fsm.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        ),RunTimeOfAction.runOnExit);


    }



}
