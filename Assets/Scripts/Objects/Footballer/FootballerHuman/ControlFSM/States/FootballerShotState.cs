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

        shotAction = new MyAction(FootballerActionMethods.ShotMethod,0.2f, 0.75f);
        AddAction(shotAction, RunTimeOfAction.runOnEnter);

        AddAction(new MyAction(FootballerActionMethods.SetAnimatorShotParameter)
            , RunTimeOfAction.runOnEnter);

        
        AddTransition(new Transition(FoorballerFallState.Instance, ConditionMethods.FallCommand));
        

        AddTransition(new Transition(FootballerIdleState.Instance, (fsm) => {
            return shotAction.ActionOver(fsm);
        }));
        

    }
    


}
