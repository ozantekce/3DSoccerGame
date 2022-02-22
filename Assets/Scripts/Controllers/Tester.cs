using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{



    private Movement movement;

    private Inputter inputter;


    public GameObject ball;


    void Start()
    {
     
        movement = GetComponent<Movement>();
        inputter = GetComponent<Inputter>();


    }

    void Update()
    {


        if (inputter == null)
            return;

        //movement.MyMovePositionWithoutY_Axis(ball.transform.position, 10);

        SpinForBall();
        
        SetVelocity(Axis.x, 10 * -inputter.GetJoyStickVerticalValue());
        SetVelocity(Axis.z, 10 * inputter.GetJoyStickHorizontalValue());   
        


        if (inputter.GetButtonShootValue() > 0)
        {
            
        }

        if (inputter.GetButtonPassValue() > 0)
        {
            
        }

        if (inputter.GetButtonSlideValue() > 0)
        {
            
        }


    }






    public void SetVelocity(Axis axis, float value)
    {
        
        movement.SetSpecificAxisVelocity(axis, value);


    }





    public float spinSpeed = 500;
    public float slightGap = 10;//ignore low differences
    private void SpinForBall()
    {
        
        Vector3 temp = transform.position - ball.transform.position;
        temp =  temp.normalized;
        Vector2 angleVector = new Vector2();
        angleVector.x = temp.z;
        angleVector.y = temp.x;
        SpinToZXVector(-angleVector);

    }

    private float spinValue;
    private Vector3 eulerAng;
    // we can use here angular velocity
    private void SpinToZXVector(Vector2 angleVector)
    {

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

        movement.Spin(spinValue, Axis.y);

        //transform.eulerAngles = eulerAng;

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
