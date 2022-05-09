using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperRunToBallState : State
{

    private readonly static GoalkeeperRunToBallState instance = new GoalkeeperRunToBallState();
    public static GoalkeeperRunToBallState Instance => instance;
    private GoalkeeperRunToBallState()
    {


    }

    public override void Init()
    {

        AddAction(new MyAction(GoalkeeperActionMethods.GoToBall, ConditionMethods.NoBallGrabbed));

        AddTransition(new Transition(GoalkeeperGrabBallState.Instance,ConditionMethods.BallGrabbable));
        //?
        AddTransition(new Transition(GoalkeeperIdleState.Instance, ConditionMethods.GoalpostFar));


    }



    public override void ExitOptional(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Animator animator = goalkeeper.Animator;

        animator.SetInteger("Walking", 0);


    }


}
