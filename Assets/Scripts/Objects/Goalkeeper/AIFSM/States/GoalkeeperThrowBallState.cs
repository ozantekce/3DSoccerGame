using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperThrowBallState : State
{

    private readonly static GoalkeeperThrowBallState instance = new GoalkeeperThrowBallState();
    public static GoalkeeperThrowBallState Instance => instance;
    private GoalkeeperThrowBallState()
    {
        Debug.Log("GoalkeeperThrowBallState created");


    }

    public override void Init(FiniteStateMachine fsm)
    {

        AddAction(ActionMethods.GoalkeeperLookRivalGoalPost);
        AddAction(ActionMethods.GoalkeeperThrowBall,ConditionMethods.Shooting, 1.6f, 2f);
        AddAction(ActionMethods.GoalkeeperHoldTheBall);

        AddTransition(GoalkeeperIdleState.Instance, ConditionMethods.Elapsed4SecondInState);

    }

    public override void Enter(FiniteStateMachine fsm)
    {

        Debug.Log("Enter GoalkeeperThrowBallState");

        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Throw");
        fsm.GetComponent<Shotable>().IsShooting = true;


    }

    public override void Exit(FiniteStateMachine fsm)
    {
        Debug.Log("Exit GoalkeeperThrowBallState");




    }




}
