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



    public void HitTheBall(Vector3 vector)
    {

        Rigidbody.AddForce(vector, ForceMode.VelocityChange);
    }
    
    public void HitTheBall_(Vector3 vector)
    {
        rigidbody.AddForce(vector, ForceMode.VelocityChange);
    }


    public void Shot(Vector3 vector) {


        rigidbody.velocity += vector;
        isShoted = true;

    }

    public Vector3 GetVelocity()
    {
        return rigidbody.velocity;
    }

    







}

