using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperAIFSM : GoalkeeperFSM
{

    public void Start()
    {
        base.Start();
        ChangeCurrentState(GoalkeeperIdleState.Instance);


    }






}
