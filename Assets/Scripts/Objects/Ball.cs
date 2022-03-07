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

    [SerializeField]
    private float maxDistanceWithOwner = 4f;

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    private GameObject owner;

    public void SetOwner(GameObject gameObject)
    {

        owner = gameObject;

    }

    public void RemoveOwner(GameObject gameObject)
    {
        if(owner == gameObject)
            owner = null;
    }

    public bool IsOwner(GameObject gameObject)
    {
        return owner == gameObject;
    }



    private Rigidbody rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();


    }

    private void Update()
    {


        if (DistanceWithOwner(owner) > maxDistanceWithOwner)
        {
            RemoveOwner(owner);
        }


    }
    


    private float DistanceWithOwner(GameObject owner) {

        if (owner == null)
            return -1;
        else
            return Vector3.Distance(owner.transform.position,this.transform.position); 
    
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

