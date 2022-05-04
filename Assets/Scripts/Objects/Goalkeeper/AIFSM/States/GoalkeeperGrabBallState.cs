using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperGrabBallState : State
{

    private readonly static GoalkeeperGrabBallState instance = new GoalkeeperGrabBallState();
    public static GoalkeeperGrabBallState Instance => instance;
    private GoalkeeperGrabBallState()
    {
        Debug.Log("GoalkeeperGrabBallState created");


    }

    public override void Init(FiniteStateMachine fsm)
    {

        AddAction(ActionMethods.GoalkeeperGoToBall, ConditionMethods.BallNotGrabbed);
        AddAction(ActionMethods.GoalkeeperGrabBall, ConditionMethods.BallGrabbableAndBallNotGrabbed);

        AddTransition(GoalkeeperIdleState.Instance, ConditionMethods.NoBallSoClose);

        AddTransition(GoalkeeperIdleWithBallState.Instance, ConditionMethods.BallGrabbed);


    }

    public override void Enter(FiniteStateMachine fsm)
    {

        Debug.Log("Enter GoalkeeperGrabBallState");



    }

    public override void Exit(FiniteStateMachine fsm)
    {
        Debug.Log("Exit GoalkeeperGrabBallState");


        Rigidbody rigidbody = fsm.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;


    }




}
