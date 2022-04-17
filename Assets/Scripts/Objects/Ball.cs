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
        cooldownToShot = new Cooldown(100f);
    }


    private Cooldown cooldownToHit;
    public bool HitTheBall(Vector3 vector)
    {
        if (cooldownToHit.Ready())
        {
            rb.AddForce(vector, ForceMode.VelocityChange);
            return true;
        }else
            return false;
    }
    
    public void HitTheBall_(Vector3 vector)
    {
        rb.AddForce(vector, ForceMode.VelocityChange);
    }


    private Cooldown cooldownToShot;
    public bool Shot(Vector3 vector) {

        if (cooldownToShot.Ready())
        {
            rb.velocity += vector;
            return true;
        }
        else
            return false;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }


    public bool IsShoted()
    {
        return !cooldownToShot.Peek();
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

