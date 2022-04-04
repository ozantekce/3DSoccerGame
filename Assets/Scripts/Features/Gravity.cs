using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("Features/Gravity")]
public class Gravity : MonoBehaviour
{

    public static readonly float GLOBAL_GRAVITY = 0.2f;

    [SerializeField]
    private float localGravity;

    [SerializeField]
    private GravityType gravityType;

    public enum GravityType
    {
        global, local
    }

    private Rigidbody rb;

    public GravityType GravityType_ { get => gravityType; set => gravityType = value; }
    public float LocalGravity { get => localGravity; set => localGravity = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;   
    }


    private void FixedUpdate()
    {
        float tempGravity = GLOBAL_GRAVITY;
        if (gravityType == GravityType.local)
            tempGravity = localGravity;

        rb.AddForce(tempGravity * Vector3.down, ForceMode.VelocityChange);

    }


}
