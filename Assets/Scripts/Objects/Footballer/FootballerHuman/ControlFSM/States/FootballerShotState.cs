using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerShotState : State
{


    private readonly static FootballerShotState instance = new FootballerShotState();
    public static FootballerShotState Instance => instance;
    private FootballerShotState()
    {
        //Debug.Log("FootballerShotState created");
    }


    MyAction shotAction;
    public override void Init()
    {

        AddTransition(new Transition(FoorballerFallState.Instance, ConditionMethods.FallCommand));

        shotAction = new MyAction(ActionMethods.ShotMethod,ConditionMethods.Shooting,0.2f,0.75f);

        AddAction(shotAction,RunTimeOfAction.runOnEnter);

        AddTransition(new Transition(FootballerIdleState.Instance,(fsm)=>{

            return shotAction.ActionOver(fsm);
        })
        );



    }



    public override void Enter_(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FootballerShotState");

        Footballer player = fsm.GetComponent<Footballer>();
        player.IsShooting = true;

        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Shot");

    }

    public override void Exit_(FiniteStateMachine fsm)
    {
        //Debug.Log("Exit FootballerShotState");


    }


}
