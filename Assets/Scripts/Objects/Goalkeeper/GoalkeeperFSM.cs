using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperFSM : PlayerFSM
{

    private Goalkeeper goalkeeper;

    public Goalkeeper Goalkeeper { get => goalkeeper; set => goalkeeper = value; }

    public void Start()
    {
        base.Start();
        goalkeeper = GetComponent<Goalkeeper>();

    }


}
