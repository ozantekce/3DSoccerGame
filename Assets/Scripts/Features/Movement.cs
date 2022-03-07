using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{

    public bool onGround = true;

    [SerializeField]
    private float movementSpeed, spinSpeed;

    private Inputter inputter;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputter = GetComponent<Inputter>();

    }

    private void Update()
    {

        if(inputter != null)
        {
            float inputVertical = -inputter.GetJoyStickVerticalValueRaw();
            float inputHorizontal = inputter.GetJoyStickHorizontalValueRaw();
            
            if(onGround && !Ball.Instance.IsOwner(gameObject))
                Movement_(inputVertical,inputHorizontal);
        }


    }
    

    private void Movement_(float inputVertical, float inputHorizontal)
    {
        Vector3 directionVector = new Vector3(inputVertical, 0, inputHorizontal).normalized;

        directionVector *= movementSpeed;
        directionVector.y = rb.velocity.y;
        rb.velocity = directionVector ;
        Spin(inputVertical,inputHorizontal);


    }


    public void MyMovePosition(Vector3 position, float speed)
    {

        if (transform.position != position)
        {
            Vector3 directionVector = position - transform.position;
            directionVector = directionVector.normalized;
            rb.velocity = directionVector * speed;

        }

    }

    public void MyMovePositionWithoutY_Axis(Vector3 position, float speed)
    {

        if (transform.position != position)
        {
            Vector3 directionVector = position - transform.position;
            directionVector = directionVector.normalized;
            directionVector.y = 0;
            rb.velocity = directionVector * speed;

        }


    }







    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }





    private float slightGap = 10;//ignore low differences


    private Vector3 angles;

    private float spinValue;
    private Vector3 eulerAng;
    // we can use here angular velocity
    private void Spin(float inputVertical, float inputHorizontal)
    {
        Vector2 angleVector = new Vector2(inputHorizontal, inputVertical);

        eulerAng = transform.eulerAngles;
        float targetAngle = CalculateAngle(angleVector);

        float gap = Mathf.Abs(targetAngle - eulerAng.y);

        if (targetAngle == -1 || gap < slightGap)
            return;

        float speed = Mathf.Clamp(spinSpeed * Time.deltaTime, 0, gap);

        bool condition = gap < 180;

        spinValue = 0;

        if (eulerAng.y < targetAngle)
        {
            if (condition)
                spinValue = speed;
            else
                spinValue = -speed;
        }
        else
        {
            if (condition)
                spinValue = -speed;
            else
                spinValue = speed;
        }

        angles = transform.eulerAngles;
        angles += spinValue * anglesMap[Axis.y];

        transform.eulerAngles = angles;
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

    private static float CalculateAngle(Vector2 vector)
    {
        if (vector == Vector2.zero) return -1;

        float angle = Mathf.Atan2(vector.y, vector.x);
        angle = Mathf.Rad2Deg * angle;
        if (angle < 0)
            angle += 360;

        return angle;
    }




}
