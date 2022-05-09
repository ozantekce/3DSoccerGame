using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperIdleWithBallState : State
{


    private readonly static GoalkeeperIdleWithBallState instance = new GoalkeeperIdleWithBallState();
    public static GoalkeeperIdleWithBallState Instance => instance;
    private GoalkeeperIdleWithBallState()
    {

    }

    public override void Init()
    {

        //  TRANSITIONS
        //  #1
        AddTransition(new Transition(GoalkeeperThrowBallState.Instance
            , GoalkeeperConditionMethods.Elapsed2Seconds));


        //  ACTIONS
        //  #1
        AddAction(new MyAction(GoalkeeperActionMethods.LookRivalGoalpost));
        //  #2
        AddAction(new MyAction(GoalkeeperActionMethods.HoldTheBall));
        //  #3
        AddAction(new MyAction(GoalkeeperActionMethods.Stop));


    }


    public override void EnterOptional(FiniteStateMachine fsm)
    {
        base.ExitOptional(fsm);

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Animator animator = goalkeeper.Animator;
        animator.SetBool("Catched", true);

    }

    public override void ExitOptional(FiniteStateMachine fsm)
    {
        base.ExitOptional(fsm);

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Animator animator = goalkeeper.Animator;
        animator.SetBool("Catched", false);

    }


}
