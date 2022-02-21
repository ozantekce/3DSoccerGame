using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{



    private Movement movement;

    private Inputter inputter;

    void Start()
    {
     
        movement = GetComponent<Movement>();
        inputter = GetComponent<Inputter>();


    }

    void Update()
    {


        if (inputter == null)
            return;

        SetVelocity(Axis.x, 10 * -inputter.GetJoyStickVerticalValue());
        SetVelocity(Axis.z, 10 * inputter.GetJoyStickHorizontalValue());



        if (inputter.GetButtonShootValue() > 0)
        {
            
        }

        if (inputter.GetButtonPassValue() > 0)
        {
            
        }

        if (inputter.GetButtonSlideValue() > 0)
        {
            
        }


    }

    public void SetVelocity(Axis axis, float value)
    {
        
        movement.SetSpecificAxisVelocity(axis, value);


    }





}
