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

        fallAction = new MyAction(FootballerActionMethods.FallMethod
            ,1.5f, 1f);

        AddAction(fallAction,RunTimeOfAction.runOnEnter);


        AddTransition(new Transition(FootballerIdleState.Instance, (fsm) => {

            return fallAction.ActionOver(fsm);
        })
        );
        


    }



    public override void EnterOptional(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FoorballerFallState");

        Footballer footballer = ((FootballerFSM)fsm).Footballer;

        footballer.IsFalling = true;
        footballer.FallCommand = false;

        Animator animator = footballer.Animator;
        animator.SetTrigger("Fall");

        footballer.Rigidbody.isKinematic = true;
        footballer.GetComponent<Collider>().isTrigger = true;

    }

    public override void ExitOptional(FiniteStateMachine fsm)
    {

        Footballer footballer = ((FootballerFSM)fsm).Footballer;

        footballer.Rigidbody.isKinematic = false;
        footballer.IsFalling = false;
        footballer.GetComponent<Collider>().isTrigger = false;

    }


}
