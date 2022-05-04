using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperJumpState : State
{

    private readonly static GoalkeeperJumpState instance = new GoalkeeperJumpState();
    public static GoalkeeperJumpState Instance => instance;
    private GoalkeeperJumpState()
    {
        Debug.Log("GoalkeeperJumpState created");


    }

    public override void Init(FiniteStateMachine fsm)
    {

        AddAction(ActionMethods.GoalkeeperGrabBall);

        //AddTransition(GoalkeeperIdleState.Instance, ConditionMethods.NoBallGrabbed);
        //AddTransition(GoalkeeperIdleWithBallState.Instance, ConditionMethods.BallGrabbed);

    }

    public override void Enter(FiniteStateMachine fsm)
    {

        Debug.Log("Enter GoalkeeperJumpState");


    }

    public override void Exit(FiniteStateMachine fsm)
    {
        Debug.Log("Exit GoalkeeperJumpState");


        Rigidbody rigidbody = fsm.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;


    }




}
