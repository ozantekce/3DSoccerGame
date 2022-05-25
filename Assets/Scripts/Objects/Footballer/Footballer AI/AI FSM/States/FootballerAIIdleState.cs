using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerAIIdleState : State
{


    private readonly static FootballerAIIdleState instance = new FootballerAIIdleState();
    public static FootballerAIIdleState Instance => instance;
    private FootballerAIIdleState()
    {

    }

    public override void Init()
    {

        //  TRANSITIONS
        //  #1
        AddTransition(new Transition(FootballerAIRunToGoalpostState.Instance,
            FootballerConditionMethods.ControlBall));
        //  #2
        AddTransition(new Transition(FootballerAIRunToBallState.Instance,
            FootballerConditionMethods.NoControlBall));
        //  #3
        /*
        AddTransition(new Transition(FootballerAIShotState.Instance,
            FootballerConditionMethods.GoalpostClose));
        */


        //  ACTIONS
        //  #1

    }





}
