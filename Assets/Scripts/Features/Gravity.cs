using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{

    public static readonly float GLOBAL_GRAVITY = 1.5f;

    [SerializeField]
    private float localGravity;

    [SerializeField]
    private GravityType gravityType;

    private enum GravityType
    {
        global, local
    }

    private Rigidbody rb;

    private GravityType GravityType_ { get => gravityType; set => gravityType = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;   
    }


    private void Update()
    {
        float tempGravity = GLOBAL_GRAVITY;
        if (gravityType == GravityType.local)
            tempGravity = localGravity;

        rb.AddForce(tempGravity * Vector3.down, ForceMode.Acceleration);

    }


}
