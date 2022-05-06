using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerSlideState : State
{


    private readonly static FootballerSlideState instance = new FootballerSlideState();
    public static FootballerSlideState Instance => instance;
    private FootballerSlideState()
    {
        //Debug.Log("FootballerSlideState created");
    }


    MyAction slideAction;
    public override void Init()
    {

        slideAction = new MyAction(ActionMethods.SlideMethod, ConditionMethods.Sliding, 0.2f, 1f);

        AddTransition(new Transition(FootballerIdleState.Instance, (fsm) => {

            return slideAction.ActionOver(fsm);
        }
        )
        );

        AddAction(slideAction,RunTimeOfAction.runOnEnter);


    }



    public override void Enter_(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FootballerSlideState");

        Footballer player = fsm.GetComponent<Footballer>();
        player.IsSliding = true;
        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Slide");
        player.GetComponentInChildren<Dropper>().IsActive = true;

    }

    public override void Exit_(FiniteStateMachine fsm)
    {
        //Debug.Log("Exit FootballerSlideState");
        fsm.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Footballer player = fsm.GetComponent<Footballer>();
        player.GetComponentInChildren<Dropper>().IsActive = false;

    }


}