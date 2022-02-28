using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float gravity;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;   
    }


    private void Update()
    {
        rb.AddForce(gravity* Vector3.down, ForceMode.Acceleration);
    }

}
