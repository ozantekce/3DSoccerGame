using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerHuman : Footballer
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

        verticalInput = -Inputter.GetJoyStickVerticalValue();
        horizontalInput = Inputter.GetJoyStickHorizontalValue();
        shotInput = Inputter.GetButtonShotValue();
        passInput = Inputter.GetButtonPassValue();
        slideInput = Inputter.GetButtonSlideValue();


    }

    public void FixedUpdate()
    {
        base.FixedUpdate();
        


    }




}
