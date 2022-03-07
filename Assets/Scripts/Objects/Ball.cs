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


    //private List<GameObject> owners;

    private GameObject owner;

    public void AddOwner(GameObject gameObject)
    {

        owner = gameObject;

        /*
        if (!owners.Contains(gameObject))
        {
            owners.Add(gameObject);
        }*/

    }


    public void RemoveOwner(GameObject gameObject)
    {
        if(owner == gameObject)
            owner = null;
        /*
        if (owners.Contains(gameObject))
        {
            owners.Remove(gameObject);
        }*/

    }

    public bool IsOwner(GameObject gameObject)
    {
        return owner == gameObject;
        //return owners.Contains(gameObject);
    }

    public float CountOfOwners()
    {
        if (owner == null)
            return 0;
        else
            return 1;
        //return owners.Count;
    }

    //public GameObject owner;

    private Movement movement;

    private Cooldown cooldownToSetVelocity;
    private void Start()
    {

        //owners = new List<GameObject>();
        movement = GetComponent<Movement>();
        cooldownToSetVelocity = new Cooldown(100f);

    }

    private void Update()
    {

        RemoveFarOwners();

    }
    

    public void MyMovePosition(Vector3 position, float value)
    {

        if (cooldownToSetVelocity.Ready())
            movement.MyMovePositionWithoutY_Axis(position , value);

    }

    
    private void RemoveFarOwners()
    {

        if(owner != null)
            if (DistanceWithOwner(owner) > maxDistanceWithOwner)
            {
                RemoveOwner(owner);
            }

        /*
        foreach (GameObject owner in owners)
        {
            if (DistanceWithOwner(owner) > maxDistanceWithOwner)
            {
                RemoveOwner(owner);
            }
        }*/

    }


    private float DistanceWithOwner(GameObject owner) {

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

