
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerFSM : PlayerFSM
{


    private Footballer footballer;

    public Footballer Footballer { get => footballer; set => footballer = value; }

    public void Start()
    {
        base.Start();
        footballer = GetComponent<Footballer>();

    }

}
