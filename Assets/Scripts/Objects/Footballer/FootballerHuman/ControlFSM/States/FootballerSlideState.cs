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

        slideAction = new MyAction(FootballerActionMethods.SlideMethod, 0.2f, 1f);

        AddAction(slideAction, RunTimeOfAction.runOnEnter);

        AddAction(new MyAction(FootballerActionMethods.SetAnimatorSlideParameter)
            , RunTimeOfAction.runOnEnter);

        AddTransition(new Transition(FootballerIdleState.Instance, (fsm) => {

            return slideAction.ActionOver(fsm);
        }
        )
        );


    }



    public override void EnterOptional(FiniteStateMachine fsm)
    {

        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        footballer.Dropper.IsActive = true;

    }

    public override void ExitOptional(FiniteStateMachine fsm)
    {

        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        footballer.Rigidbody.velocity = VectorCalculater.VectorZeroWithoutY(footballer.Rigidbody.velocity);
        footballer.Dropper.IsActive = false;

    }


}