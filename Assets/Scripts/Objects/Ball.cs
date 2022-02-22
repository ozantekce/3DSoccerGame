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

    private GameObject owner;

    private Movement movement;
    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        
        if(distanceWithOwner() > maxDistanceWithOwner)
        {
            owner = null;
        }

    }


    public void Shoot(Vector3 velocity)
    {
        owner = null;
        movement.GiveVelocity(velocity);

    }


    

    private float distanceWithOwner() {

        if (owner == null)
            return -1;
        else
            return Vector3.Distance(owner.transform.position,this.transform.position); 
    
    }


}

