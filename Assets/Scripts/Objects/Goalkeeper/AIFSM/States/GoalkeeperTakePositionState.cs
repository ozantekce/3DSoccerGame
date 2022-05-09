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
        AddAction(new MyAction(GoalkeeperActionMethods.LookTheBall));
        AddAction(new MyAction(GoalkeeperActionMethods.TakePosition));

        AddTransition(new Transition(GoalkeeperIdleState.Instance,ConditionMethods.GoalkeeperRightPosition));


        AddTransition(new Transition(GoalkeeperJumpState.Instance, ConditionMethods.BallShotedAndGoalkeeperMeetingWithBall));

    }

    public override void EnterOptional(FiniteStateMachine fsm)
    {


    }

    public override void ExitOptional(FiniteStateMachine fsm)
    {

        Rigidbody rigidbody = fsm.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Animator animator = goalkeeper.Animator;

        animator.SetInteger("Walking", 0);

    }




}
