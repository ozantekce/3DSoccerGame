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


    public override void Init(FiniteStateMachine fsm)
    {

        AddTransition(FoorballerFallState.Instance, ConditionMethods.FallCommand);
        AddTransition(FootballerRunState.Instance, ConditionMethods.VerticalOrHorizontalInput);
        AddTransition(FootballerIdleWithBallState.Instance, ConditionMethods.ControlBall);
        AddTransition(FootballerSlideState.Instance, ConditionMethods.NoControlBallAndSlideInput);
        

    }




    public override void Enter(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter idle");

        Animator animator = fsm.GetComponent<Animator>();
        animator.SetBool("IsRunning", false);

    }

    public override void Exit(FiniteStateMachine fsm)
    {
        //Debug.Log("Exit idle");
    }




}
