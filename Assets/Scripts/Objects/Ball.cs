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
    private float maxDistanceWithOwner = 10f;

    public GameObject Owner { get => owner; set => owner = value; }

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



    private Cooldown cooldownToSetVelocity;
    private Axis lastAxis = Axis.y;
    public void SetVelocity(Axis axis, float value, float distanceWithOwner)
    {

        if (cooldownToSetVelocity.Ready())
        {
            

            if(lastAxis == axis)
            {
                if (distanceWithOwner <= 2.5f)
                {
                    movement.SetVelocity(movement.GetVelocity() / 2);
                    movement.SetSpecificAxisVelocity(axis, value);
                    lastAxis = axis;
                }
                    
            }
            else
            {
                movement.SetVelocity(movement.GetVelocity() / 2);
                movement.SetSpecificAxisVelocity(axis, value);
                lastAxis = axis;
            }
                
            
        }

        


    }


    public void MyMovePosition(Vector3 position, float value)
    {

        if (cooldownToSetVelocity.Ready())
            movement.MyMovePositionWithoutY_Axis(position , value);


    }


    public void Shoot(Vector3 velocity)
    {
        owner = null;
        movement.GiveVelocity(velocity);

    }


    public void Pass(Vector3 targetPosition,float velocity)
    {

        owner = null;
        movement.MyMovePosition(targetPosition, velocity);

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

