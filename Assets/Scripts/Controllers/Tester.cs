using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{



    private Movement movement;

    void Start()
    {
     
        movement = GetComponent<Movement>();

    }

    void Update()
    {


        movement.GiveVelocity(
            new Vector3(-Input.GetAxis("Vertical") *10f, 0, Input.GetAxis("Horizontal")*10f)
            );

        movement.Spin(1, Axis.y);
    }




    




}
