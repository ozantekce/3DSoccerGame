using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerControlFSM : FiniteStateMachine
{


    public void Awake()
    {

        CurrentState = FootballerIdleState.Instance;


    }





}
