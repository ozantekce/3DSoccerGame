using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoorballerFallState : State
{


    private readonly static FoorballerFallState instance = new FoorballerFallState();
    public static FoorballerFallState Instance => instance;
    private FoorballerFallState()
    {
        //Debug.Log("FoorballerFallState created");
    }


    MyAction fallAction;
    public override void Init(FiniteStateMachine fsm)
    {

        fallAction = new MyAction(ActionMethods.FallMethod, ConditionMethods.Falling, 1.5f, 0.1f);

        AddTransition(FootballerIdleState.Instance, (fsm) => {

            return fsm.ActionOver(fallAction);
        }
        );

        AddAction(fallAction);


    }



    public override void Enter(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FoorballerFallState");
        Footballer player = fsm.GetComponent<Footballer>();
        player.IsFalling = true;
        player.FallCommand = false;
        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Fall");

    }

    public override void Exit(FiniteStateMachine fsm)
    {
        //Debug.Log("Exit FoorballerFallState");


    }


}
