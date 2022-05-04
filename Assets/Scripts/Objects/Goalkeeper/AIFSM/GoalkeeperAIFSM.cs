using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperAIFSM : FiniteStateMachine
{



    public void Awake()
    {

        CurrentState = GoalkeeperIdleState.Instance;

    }






}
