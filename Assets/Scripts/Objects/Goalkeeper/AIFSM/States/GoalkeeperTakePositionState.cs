using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperTakePositionState : State
{

    private readonly static GoalkeeperTakePositionState instance = new GoalkeeperTakePositionState();
    public static GoalkeeperTakePositionState Instance => instance;
    private GoalkeeperTakePositionState()
    {
        Debug.Log("GoalkeeperTakePositionState created");


    }

    public override void Init(FiniteStateMachine fsm)
    {

        AddAction(ActionMethods.GoalkeeperTakePosition);

        AddTransition(GoalkeeperIdleState.Instance,ConditionMethods.GoalkeeperRightPosition);
        AddTransition(GoalkeeperGrabBallState.Instance, ConditionMethods.BallSoClose);
        AddTransition(GoalkeeperJumpState.Instance, ConditionMethods.BallShotedAndGoalkeeperMeetingWithBall);

    }

    public override void Enter(FiniteStateMachine fsm)
    {
        Debug.Log("Enter GoalkeeperTakePositionState");

    }

    public override void Exit(FiniteStateMachine fsm)
    {
        Debug.Log("Exit GoalkeeperTakePositionState");
        Rigidbody rigidbody = fsm.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;

        Animator animator = fsm.GetComponent<Animator>();

        animator.SetBool("WalkingLeft", false);
        animator.SetBool("WalkingRight", false);

    }




}
