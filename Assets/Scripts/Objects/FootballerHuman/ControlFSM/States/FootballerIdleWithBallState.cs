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


    public override void Init(FiniteStateMachine fsm)
    {

        AddTransition(FoorballerFallState.Instance, ConditionMethods.FallCommand);

        AddTransition(FootballerIdleState.Instance, ConditionMethods.NoControlBall);

        AddTransition(FootballerDribblingState.Instance, ConditionMethods.ControlBallAndVerticalOrHorizontalInput);

        AddTransition(FootballerShotState.Instance, ConditionMethods.ShotInput);

        AddTransition(FootballerPassState.Instance, ConditionMethods.PassInput);


    }




    public override void Enter(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FootballerIdleWithBallState");
        Animator animator = fsm.GetComponent<Animator>();
        animator.SetBool("IsRunning", false);
    }

    public override void Exit(FiniteStateMachine fsm)
    {
        //Debug.Log("Exit FootballerIdleWithBallState");

    }




}

