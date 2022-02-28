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

    [SerializeField]
    private float maxDistanceWithOwner = 5f;


    public GameObject Owner { get => owner; set => owner = value; }
    public Movement Movement { get => movement; set => movement = value; }

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }



    public GameObject owner;

    private Movement movement;

    private Cooldown cooldownToSetVelocity;
    private void Start()
    {
        movement = GetComponent<Movement>();
        cooldownToSetVelocity = new Cooldown(100f);



    }

    private void Update()
    {
        
        if(distanceWithOwner() > maxDistanceWithOwner)
        {
            owner = null;
        }

    }




    public void MyMovePosition(Vector3 position, float value)
    {

        if (cooldownToSetVelocity.Ready())
            movement.MyMovePositionWithoutY_Axis(position , value);

    }





    private float distanceWithOwner() {

        if (owner == null)
            return -1;
        else
            return Vector3.Distance(owner.transform.position,this.transform.position); 
    
    }



    public Vector3 GetVelocity()
    {
        return movement.GetVelocity();
    }

    public float GetDrag()
    {
        return movement.GetDrag();
    }

}

