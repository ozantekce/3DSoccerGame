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



    public override void Init(FiniteStateMachine fsm)
    {

        AddTransition(FoorballerFallState.Instance, ConditionMethods.FallCommand);
        AddTransition(FootballerRunState.Instance, ConditionMethods.NoControlBallAndVerticalOrHorizontalInput);
        AddTransition(FootballerIdleState.Instance, ConditionMethods.NoControlBallAndNoVerticalOrHorizontalInput);
        AddTransition(FootballerIdleWithBallState.Instance, ConditionMethods.ControlBallAndNoVerticalOrHorizontalInput);
        
        AddTransition(FootballerShotState.Instance, ConditionMethods.ShotInput);
        AddTransition(FootballerPassState.Instance, ConditionMethods.PassInput);

        AddAction(ActionMethods.FootballerDribblingMethod);


    }

    public override void Enter(FiniteStateMachine fsm)
    {
        //Debug.Log("Enter FootballerDribblingState");
        Animator animator = fsm.GetComponent<Animator>();
        animator.SetBool("IsRunning", true);

    }

    public override void Exit(FiniteStateMachine fsm)
    {
        //Debug.Log("Exit FootballerDribblingState");

        fsm.GetComponent<Rigidbody>().velocity = fsm.gameObject.transform.forward*0.5f;
        Vector3 directionVector = VectorCalculater.CalculateDirectionVector(Ball.Instance.GetVelocity(), Vector3.zero);
        
        Ball.Instance.HitTheBall_(directionVector * fsm.GetComponent<Footballer>().DribblingPower);

    }


}
