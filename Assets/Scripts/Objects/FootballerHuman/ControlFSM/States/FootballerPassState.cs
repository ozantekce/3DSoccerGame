using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerPassState : State
{


    private readonly static FootballerPassState instance = new FootballerPassState();
    public static FootballerPassState Instance => instance;
    private FootballerPassState()
    {
        //Debug.Log("FootballerPassState created");
    }


    MyAction passAction;
    public override void Init(FiniteStateMachine fsm)
    {

        AddTransition(FoorballerFallState.Instance, ConditionMethods.FallCommand);

        passAction = new MyAction(ActionMethods.PassMethod, ConditionMethods.Passing, 0.2f, 0.5f);

        AddTransition(FootballerIdleState.Instance, (fsm) => {
            return fsm.ActionOver(passAction);
        });

        AddAction(passAction);

    }



    public override void Enter(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FootballerPassState");
        Footballer player = fsm.GetComponent<Footballer>();
        player.IsPassing = true;
        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Pass");

    }

    public override void Exit(FiniteStateMachine fsm)
    {

        //Debug.Log("Exit FootballerPassState");

    }


}
