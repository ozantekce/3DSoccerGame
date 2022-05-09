using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperGoTowardsTheBallState : State
{

    private readonly static GoalkeeperGoTowardsTheBallState instance = new GoalkeeperGoTowardsTheBallState();
    public static GoalkeeperGoTowardsTheBallState Instance => instance;
    private GoalkeeperGoTowardsTheBallState()
    {


    }

    public override void Init()
    {

        //  TRANSITIONS
        //  #1
        AddTransition(new Transition(GoalkeeperGrabBallState.Instance
            , GoalkeeperConditionMethods.BallGrabbable));
        //  #2
        AddTransition(new Transition(GoalkeeperTakePositionState.Instance
            , GoalkeeperConditionMethods.GoalpostFar));


        //  ACTIONS
        //  #1
        AddAction(new MyAction(GoalkeeperActionMethods.GoToBallPosition));


    }



    public override void ExitOptional(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Animator animator = goalkeeper.Animator;

        animator.SetInteger("Walking", 0);


    }


}
