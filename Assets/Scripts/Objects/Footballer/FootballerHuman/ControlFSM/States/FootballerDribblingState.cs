using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerDribblingState : State
{


    private readonly static FootballerDribblingState instance = new FootballerDribblingState();
    public static FootballerDribblingState Instance => instance;
    private FootballerDribblingState()
    {
        //Debug.Log("FootballerDribblingState created");
    }



    public override void Init()
    {

        AddTransition(new Transition(FoorballerFallState.Instance, FootballerConditionMethods.FallCommand));

        AddTransition(new Transition(FootballerRunState.Instance, FootballerConditionMethods.NoControlBallAndVerticalOrHorizontalInput));
        
        AddTransition(new Transition(FootballerIdleState.Instance, FootballerConditionMethods.NoControlBallAndNoVerticalOrHorizontalInput));
        
        AddTransition(new Transition(FootballerIdleWithBallState.Instance, FootballerConditionMethods.ControlBallAndNoVerticalOrHorizontalInput));
        
        AddTransition(new Transition(FootballerShotState.Instance, FootballerConditionMethods.ShotInput));
        
        AddTransition(new Transition(FootballerPassState.Instance, FootballerConditionMethods.PassInput));

        AddAction(new MyAction(FootballerActionMethods.DribblingMethod));

        AddAction(new MyAction(FootballerActionMethods.SetAnimatorRunningParameterToTrue)
            ,RunTimeOfAction.runOnEnter);


    }


    public override void ExitOptional(FiniteStateMachine fsm)
    {

        fsm.GetComponent<Rigidbody>().velocity = fsm.gameObject.transform.forward*0.5f;
        Vector3 directionVector 
            = VectorCalculater.CalculateDirectionVector(Ball.Instance.GetVelocity(), Vector3.zero);
        
        Ball.Instance.HitTheBall_(directionVector * fsm.GetComponent<Footballer>().DribblingPower);

    }


}
