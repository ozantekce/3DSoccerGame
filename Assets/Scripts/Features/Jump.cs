using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{



    public bool onGround = true;

    private bool jumpFinished = true;

    [SerializeField]
    private float jumpPower;


    [SerializeField]
    private float cdJump = 500f;
    private CooldownManualReset cooldownForJump;

    private Inputter inputter;
    private Rigidbody rb;


    private void Start()
    {
        cooldownForJump = new CooldownManualReset(cdJump);
        inputter = GetComponent<Inputter>();
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {   

        if (inputter.GetButtonJumpValue() > 0)
        {
            Vector3 direction = Vector3.up;
            direction.x = -inputter.GetJoyStickVerticalValueRaw();
            direction.z = inputter.GetJoyStickHorizontalValueRaw();

            Jump_(direction.normalized * jumpPower, 0.2f);
        }

    }


    public void Jump_(Vector3 vector3,float wait)
    {
        if (cooldownForJump.TimeOver() && onGround && jumpFinished)
        {
            StartCoroutine(Jump__(vector3,wait));
        }

    }


    private IEnumerator Jump__(Vector3 vector3, float wait)
    {
        jumpFinished = false;
        yield return new WaitForSeconds(wait);
        
        rb.velocity = vector3;
        print(rb.velocity);
        cooldownForJump.ResetTimer();

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
