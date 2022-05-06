using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperTakePositionState : State
{

    private readonly static GoalkeeperTakePositionState instance = new GoalkeeperTakePositionState();
    public static GoalkeeperTakePositionState Instance => instance;
    private GoalkeeperTakePositionState()
    {



    }

    public override void Init()
    {

        AddAction(new MyAction(ActionMethods.GoalkeeperTakePosition));

        AddTransition(new Transition(GoalkeeperIdleState.Instance,ConditionMethods.GoalkeeperRightPosition));
        AddTransition(new Transition(GoalkeeperGrabBallState.Instance, ConditionMethods.BallSoClose));
        AddTransition(new Transition(GoalkeeperJumpState.Instance, ConditionMethods.BallShotedAndGoalkeeperMeetingWithBall));

    }

    public override void Enter_(FiniteStateMachine fsm)
    {


    }

    public override void Exit_(FiniteStateMachine fsm)
    {

        Rigidbody rigidbody = fsm.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;

        Animator animator = fsm.GetComponent<Animator>();

        animator.SetBool("WalkingLeft", false);
        animator.SetBool("WalkingRight", false);

    }




}
