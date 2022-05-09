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

        passAction = new MyAction(FootballerActionMethods.PassMethod,0.2f, 0.5f);

        AddAction(passAction, RunTimeOfAction.runOnEnter);

        AddAction(new MyAction(FootballerActionMethods.SetAnimatorPassParameter),RunTimeOfAction.runOnEnter);

        AddTransition(new Transition(FoorballerFallState.Instance, ConditionMethods.FallCommand));


        AddTransition(new Transition(FootballerIdleState.Instance, (fsm) => {
            return passAction.ActionOver(fsm);
        }));


    }



    public override void EnterOptional(FiniteStateMachine fsm)
    {
        base.EnterOptional(fsm);
        Debug.Log("PASS");

    }

}
