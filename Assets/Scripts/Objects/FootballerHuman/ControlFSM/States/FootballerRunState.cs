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



    public override void Init(FiniteStateMachine fsm)
    {

        AddTransition(FoorballerFallState.Instance, ConditionMethods.FallCommand);

        AddTransition(FootballerIdleState.Instance, ConditionMethods.NoVerticalOrHorizontalInput);
        AddTransition(FootballerDribblingState.Instance, ConditionMethods.ControlBallAndVerticalOrHorizontalInput);
        AddTransition(FootballerSlideState.Instance, ConditionMethods.NoControlBallAndSlideInput);

        AddAction(ActionMethods.FootballerRunMethod);


    }



    public override void Enter(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter run");
        Animator animator = fsm.GetComponent<Animator>();
        animator.SetBool("IsRunning", true);

    }

    public override void Exit(FiniteStateMachine fsm)
    {
        //Debug.Log("Exit run");
        fsm.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }


}
