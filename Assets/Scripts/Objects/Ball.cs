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

    private Collider collider;

    private Rigidbody rigidbody;
    public Rigidbody Rigidbody { get => rigidbody; set => rigidbody = value; }
    public bool IsShoted { get => isShoted; set => isShoted = value; }
    public Collider Collider { get => collider; set => collider = value; }

    private bool isShoted;

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    private const float resetIsShotedDelay = 2f;
    private float resetIsShotedDelayTimer = 0;
    private void FixedUpdate()
    {
        limit--;
        if (isShoted)
        {
            resetIsShotedDelayTimer += Time.deltaTime;
            if(resetIsShotedDelayTimer >= resetIsShotedDelay)
            {
                IsShoted = false;
            }
        }
        else
        {
            resetIsShotedDelayTimer = 0;
        }
        
    }




    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }


    private int limit = 30;
    public void HitTheBall(Vector3 vector)
    {
        if(limit <= 0)
        {
            Rigidbody.AddForce(vector, ForceMode.VelocityChange);
            limit = 30;
        }
        
    }

    private int limit_ = 60;

    public void HitTheBall_(Vector3 vector)
    {
        if (limit <= 0)
        {
            Rigidbody.AddForce(vector, ForceMode.VelocityChange);
            limit = 60;
        }
    }


    public void Shot(Vector3 vector) {


        rigidbody.velocity += vector;
        isShoted = true;

    }

    public Vector3 GetVelocity()
    {
        return rigidbody.velocity;
    }




    public List<Footballer> controllers;



    public void AddController(Footballer footballer)
    {
        /*
        if(footballer.slowDown==0)
        foreach (Footballer controller in controllers)
        {

            controller.slowDown = 100;

        }
        */
        controllers.Add(footballer);


    }

    public void RemoveController(Footballer footballer)
    {

        controllers.Remove(footballer);


    }







}

