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



    private bool open;
    public void Open_Close_AI()
    {
        inputter.enabled = !inputter.enabled;
        horizontalInput = 0;
        verticalInput = 0;
        shotInput = 0;
    }


}
