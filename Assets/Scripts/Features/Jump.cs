using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
public class Jump : MonoBehaviour
{



    public bool onGround = true;
    private Movement movement;
    private bool jumpFinished = true;

    [SerializeField]
    private float maxJumpMagnitude;
    [SerializeField]
    private float cooldownTimePass = 500f;
    private CooldownManualReset cooldownForPass;




    private void Start()
    {
        movement = GetComponent<Movement>();
        cooldownForPass = new CooldownManualReset(cooldownTimePass);

    }


    public void Jump_(Vector3 vector3,float wait)
    {




        if (cooldownForPass.TimeOver() && onGround && jumpFinished)
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

        vector3 = VectorCalculater.PreventToPassMaxMagnitude(vector3, maxJumpMagnitude);
        movement.SetVelocity(vector3);
        cooldownForPass.ResetTimer();

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
