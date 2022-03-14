using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //Singleton
    private static Ball instance = null;

    public static Ball Instance
    {
        get
        {
            return instance;
        }
    }

    public Rigidbody Rb { get => rb; set => rb = value; }




    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }



    private Rigidbody rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        cooldownToHit = new Cooldown(100f);

    }


    private Cooldown cooldownToHit;
    public void HitTheBall(float axisX, float axisZ ,float maxVel)
    {



        Vector3 targetVelocity = maxVel * new Vector3 (axisX, 0, axisZ).normalized;


        Vector3 addVelocity = targetVelocity - rb.velocity;

        if(addVelocity.magnitude > maxVel)
        {
            addVelocity = addVelocity.normalized*maxVel;
        }

        rb.velocity += addVelocity;



    }


    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public void MyMovePosition(Vector3 position, float speed)
    {

        if (transform.position != position)
        {
            Vector3 directionVector = position - transform.position;
            directionVector = directionVector.normalized;
            directionVector.y = 0;
            speed = Mathf.Clamp(speed, 0, Vector3.Distance(position, transform.position));
            rb.velocity = directionVector * speed;
        }


    }





}

