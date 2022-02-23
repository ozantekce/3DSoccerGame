using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{



    private bool onGround = true;
    private Movement movement;


    private void Start()
    {
        movement = GetComponent<Movement>();
    }


    public void Jump_(Vector3 vector3)
    {

        if (onGround)
        {
            movement.GiveVelocity(vector3);
        }
        else
        {
            return;
        }

    }




    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }


}
