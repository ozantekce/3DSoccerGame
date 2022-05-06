using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperThrowBallState : State
{

    private readonly static GoalkeeperThrowBallState instance = new GoalkeeperThrowBallState();
    public static GoalkeeperThrowBallState Instance => instance;
    private GoalkeeperThrowBallState()
    {


    }

    MyAction throwTheBall;
    public override void Init()
    {

        AddAction(new MyAction(ActionMethods.GoalkeeperLookRivalGoalPost));

        throwTheBall = new MyAction(ActionMethods.GoalkeeperThrowBall, ConditionMethods.Shooting, 1.6f, 2f);

        AddAction(throwTheBall, RunTimeOfAction.runOnEnter);

        AddAction(new MyAction(ActionMethods.GoalkeeperHoldTheBall),RunTimeOfAction.runOnPreExecution);


        AddTransition(new Transition(GoalkeeperIdleState.Instance, (fsm) =>
        {
            return throwTheBall.ActionOver(fsm);
        }
        ));


    }

    public override void Enter_(FiniteStateMachine fsm)
    {


        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Throw");
        fsm.GetComponent<Shotable>().IsShooting = true;


    }

    public override void Exit_(FiniteStateMachine fsm)
    {




    }




}
