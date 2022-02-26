using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
public class Jump : MonoBehaviour
{



    public bool onGround = true;
    private Movement movement;
    private bool jumpFinished = true;


    private void Start()
    {
        movement = GetComponent<Movement>();
    }


    public void Jump_(Vector3 vector3,float wait)
    {

        if (onGround && jumpFinished)
        {
            StartCoroutine(Jump__(vector3,wait));
        }
        else
        {
            return;
        }

    }


    private IEnumerator Jump__(Vector3 vector3, float wait)
    {
        jumpFinished = false;
        yield return new WaitForSeconds(wait);
        movement.SetVelocity(vector3);
        jumpFinished = true;

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
