using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerAI : Footballer
{
    [SerializeField]
    Inputter inputter;
    public Inputter Inputter { get => inputter; set => inputter = value; }


    public void Start()
    {
        base.Start();
        inputter = GetComponent<Inputter>();


    }

    public void Update()
    {
        base.Update();



    }

    public void FixedUpdate()
    {
        base.FixedUpdate();



    }



}
