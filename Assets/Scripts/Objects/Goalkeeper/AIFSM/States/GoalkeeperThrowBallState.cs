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



    }

    public override void Enter(FiniteStateMachine fsm)
    {

        Debug.Log("Enter GoalkeeperThrowBallState");



    }

    public override void Exit(FiniteStateMachine fsm)
    {
        Debug.Log("Exit GoalkeeperThrowBallState");




    }




}
