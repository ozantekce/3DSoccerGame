using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{


    public bool includeY_axisForMaxVelocity;

    [SerializeField]
    private float maxVelocity;
    private float sqrMaxVelocity;

    private Vector3 lastVelocity;




    private Rigidbody rb;

    public float MaxVelocity
    {
        get => maxVelocity; set
        {

            maxVelocity = value;
            sqrMaxVelocity = Mathf.Max(maxVelocity, 2);

        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sqrMaxVelocity = Mathf.Pow(maxVelocity, 2);

    }

    private void LateUpdate()
    {

        EnsureMaxVelocity();

    }


    // ENSURE LIMITATIONS START


    private Vector3 tempVelocity;
    private void EnsureMaxVelocity()
    {

        if (includeY_axisForMaxVelocity&&PassedMaxSpeed())
        {
            SetVelocity(GetVelocity().normalized * maxVelocity);
        }
        else if(PassedMaxSpeed())
        {
            tempVelocity = GetVelocity().normalized * maxVelocity;
            tempVelocity.y = GetVelocity().y;
            SetVelocity(tempVelocity);

        }

    }


    // ENSURE LIMITATIONS END



    // GIVE_SET Rigidbody START
    public void GiveVelocity(Vector3 velocity)
    {

        rb.velocity += velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;

    }

    
    public void SetSpecificAxisVelocity(Axis axis,float value)
    {
        tempVelocity = GetVelocity();
        if (axis == Axis.x)
        {
            tempVelocity.x = value;  
        }
        else if(axis == Axis.y)
        {
            tempVelocity.y = value;
        }
        else
        {
            tempVelocity.z = value;
        }

        rb.velocity = tempVelocity;

    }

    public void GiveForce(Vector3 force)
    {

        rb.AddForce(force);

    }

    public void SetForce(Vector3 force)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(force);

    }


    public void GiveAcceleration(Vector3 acceleration)
    {

        rb.AddForce(acceleration, ForceMode.Acceleration);

    }

    public void SetAcceleration(Vector3 acceleration)
    {

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(acceleration, ForceMode.Acceleration);

    }


    public void SetMass(float mass)
    {
        rb.mass = mass;
    }

    // GIVE_SET Rigidbody END


    // GET Rigidbody START
    public float GetMass(){return rb.mass;}

    public Vector3 GetVelocity(){return rb.velocity;}
    // GET Rigidbody END


    // BOOL Control START

    private bool PassedMaxSpeed()
    {
        return rb.velocity.sqrMagnitude > sqrMaxVelocity;
    }


    // BOOL Control END






    public void MyMovePosition(Vector3 position, float speed, bool canPassMaxSpeed)
    {

        if (transform.position != position)
        {
            Vector3 directionVector = position - transform.position;
            directionVector = directionVector.normalized;
            SetVelocity(directionVector * speed);

        }

    }
    public void MovePosition(Vector3 position)
    {

        rb.MovePosition(position);

    }



    


    private readonly static Dictionary<Axis, Vector3> anglesMap = CreateAnglesMap();
    private static Dictionary<Axis, Vector3> CreateAnglesMap()
    {
        Dictionary<Axis, Vector3> temp = new Dictionary<Axis, Vector3>();
        temp.Add(Axis.x, Vector3.right);
        temp.Add(Axis.y, Vector3.up);
        temp.Add(Axis.z, Vector3.forward);
        return temp;
    }
    private Vector3 angles;
    public void Spin(float angle, Axis axis)
    {

        angles = transform.eulerAngles;
        angles += angle * anglesMap[axis];

        transform.eulerAngles = angles;

    }




}
