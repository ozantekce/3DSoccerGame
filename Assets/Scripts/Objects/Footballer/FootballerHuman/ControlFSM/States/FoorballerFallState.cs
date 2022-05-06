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
    public override void Init()
    {

        fallAction = new MyAction(ActionMethods.FallMethod, ConditionMethods.Falling, 1.5f, 0.1f);

        AddAction(fallAction,RunTimeOfAction.runOnEnter);




        AddTransition(new Transition(FootballerIdleState.Instance, (fsm) => {

            return fallAction.ActionOver(fsm);
        })
        );
        


    }



    public override void Enter_(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FoorballerFallState");
        
        Footballer player = fsm.GetComponent<Footballer>();
        player.IsFalling = true;
        player.FallCommand = false;
        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Fall");
        player.Rigidbody.isKinematic = true;

    }

    public override void Exit_(FiniteStateMachine fsm)
    {
        //Debug.Log("Exit FoorballerFallState");
        Footballer player = fsm.GetComponent<Footballer>();
        player.Rigidbody.isKinematic = false;

    }


}
