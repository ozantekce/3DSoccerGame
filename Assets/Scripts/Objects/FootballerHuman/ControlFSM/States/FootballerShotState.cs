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
    public override void Init(FiniteStateMachine fsm)
    {

        AddTransition(FoorballerFallState.Instance, ConditionMethods.FallCommand);

        shotAction = new MyAction(ActionMethods.ShotMethod,ConditionMethods.Shooting,0.2f,0.5f);

        AddTransition(FootballerIdleState.Instance,(fsm)=>{

            return fsm.ActionOver(shotAction);
        }
        );

        AddAction(shotAction);


    }



    public override void Enter(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FootballerShotState");
        Footballer player = fsm.GetComponent<Footballer>();
        player.IsShooting = true;

        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Shot");

    }

    public override void Exit(FiniteStateMachine fsm)
    {
        //Debug.Log("Exit FootballerShotState");


    }


}
