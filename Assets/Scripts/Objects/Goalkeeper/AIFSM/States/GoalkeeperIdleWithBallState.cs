using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperIdleWithBallState : State
{


    private readonly static GoalkeeperIdleWithBallState instance = new GoalkeeperIdleWithBallState();
    public static GoalkeeperIdleWithBallState Instance => instance;
    private GoalkeeperIdleWithBallState()
    {
        Debug.Log("GoalkeeperIdleWithBallState created");
    }

    public override void Init(FiniteStateMachine fsm)
    {

        AddAction(ActionMethods.GoalkeeperHoldTheBall);

        AddTransition(GoalkeeperThrowBallState.Instance,ConditionMethods.Elapsed2SecondInState);


    }

    public override void Enter(FiniteStateMachine fsm)
    {
        Debug.Log("Enter GoalkeeperIdleWithBallState");
        Animator animator = fsm.GetComponent<Animator>();
        //animator.SetBool("IsRunning", false);

    }

    public override void Exit(FiniteStateMachine fsm)
    {
        Debug.Log("Exit GoalkeeperIdleWithBallState");


    }





}
