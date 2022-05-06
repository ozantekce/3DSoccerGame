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
    public override void Init()
    {

        AddTransition(new Transition(FoorballerFallState.Instance, ConditionMethods.FallCommand));

        passAction = new MyAction(ActionMethods.PassMethod, ConditionMethods.Passing, 0.2f, 0.5f);

        AddTransition(new Transition(FootballerIdleState.Instance, (fsm) => {
            return passAction.ActionOver(fsm);
        }));

        AddAction(passAction,RunTimeOfAction.runOnEnter);


    }



    public override void Enter_(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FootballerPassState");
        Footballer player = fsm.GetComponent<Footballer>();
        player.IsPassing = true;
        player.Animator.SetTrigger("Pass");

    }

    public override void Exit_(FiniteStateMachine fsm)
    {

        //Debug.Log("Exit FootballerPassState");

    }


}
