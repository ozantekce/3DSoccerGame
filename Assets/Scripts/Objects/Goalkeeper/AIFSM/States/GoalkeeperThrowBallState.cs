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

        AddAction(new MyAction(GoalkeeperActionMethods.GoalkeeperLookRivalGoalPost));

        throwTheBall = new MyAction(GoalkeeperActionMethods.GoalkeeperThrowBall, 1.6f, 2f);

        AddAction(throwTheBall, RunTimeOfAction.runOnEnter);

        AddAction(new MyAction(GoalkeeperActionMethods.GoalkeeperHoldTheBall),RunTimeOfAction.runOnPreExecution);


        AddTransition(new Transition(GoalkeeperIdleState.Instance, (fsm) =>
        {
            return throwTheBall.ActionOver(fsm);
        }
        ));


    }

    public override void EnterOptional(FiniteStateMachine fsm)
    {


        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Throw");


    }

    public override void ExitOptional(FiniteStateMachine fsm)
    {




    }




}
