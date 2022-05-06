using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerControlFSM : FootballerFSM
{



    public void Start()
    {
        base.Start();
        ChangeCurrentState(FootballerIdleState.Instance);

    }





}
