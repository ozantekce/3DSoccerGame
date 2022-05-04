using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperIdleState : State
{


    private readonly static GoalkeeperIdleState instance = new GoalkeeperIdleState();
    public static GoalkeeperIdleState Instance => instance;
    private GoalkeeperIdleState()
    {
        Debug.Log("GoalkeeperIdleState created");
    }

    public override void Init(FiniteStateMachine fsm)
    {
        

        AddTransition(GoalkeeperTakePositionState.Instance,ConditionMethods.GoalkeeperWrongPosition);
        AddTransition(GoalkeeperGrabBallState.Instance, ConditionMethods.BallSoClose);
        AddTransition(GoalkeeperJumpState.Instance, ConditionMethods.GoalkeeperMeetingWithBall);


    }

    public override void Enter(FiniteStateMachine fsm)
    {
        Debug.Log("Enter GoalkeeperIdleState");
        Animator animator = fsm.GetComponent<Animator>();
        //animator.SetBool("IsRunning", false);

    }

    public override void Exit(FiniteStateMachine fsm)
    {
        Debug.Log("Exit GoalkeeperIdleState");


    }





}
