using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{




    private Movement movement;
    private Shoot shoot;

    private Inputter inputter;

    private BallVision ballVision;

    public Ball ball;

    private Cooldown shootCooldown ;


    private Jump jump;



    void Start()
    {

        shootCooldown = new Cooldown(500f);
        movement = GetComponent<Movement>();
        inputter = GetComponent<Inputter>();
        shoot = GetComponent<Shoot>();
        ballVision = GetComponent<BallVision>();
        jump = GetComponent<Jump>();
        ball = Ball.Instance;

    }

    void Update()
    {


        if (inputter == null)
            return;



        if(ball.owner == this.gameObject)
        {
            if(inputter.GetJoyStickVerticalValueRaw()!=0)
                ball.SetVelocity(Axis.x, 12 * -inputter.GetJoyStickVerticalValueRaw());
            if (inputter.GetJoyStickHorizontalValueRaw() != 0)
                ball.SetVelocity(Axis.z, 12 * inputter.GetJoyStickHorizontalValueRaw());

            CloseDistamceWithBall();




        }
        else
        {

            SetVelocity(Axis.x, 15 * -inputter.GetJoyStickVerticalValue());
            SetVelocity(Axis.z, 15 * inputter.GetJoyStickHorizontalValue());

            SpinToZXVector(new Vector2(inputter.GetJoyStickHorizontalValue(), -inputter.GetJoyStickVerticalValue()));


        }







        if (inputter.GetButtonShootValue() > 0 && shootCooldown.Ready())
        {
            ballVision.CooldownWaitToTakeBall.Reset();
            shoot.Shoot_(40*transform.forward*inputter.GetButtonShootValue(),0.3f);
        
        }

        if (inputter.GetButtonPassValue() > 0)
        {
            
        }

        if (inputter.GetButtonSlideValue() > 0)
        {
            
        }


        if (inputter.GetButtonJumpValue() > 0)
        {
            jump.Jump_(Vector3.up*1);
        }


    }


    private float minDistance = 1.6f;
    public void CloseDistamceWithBall()
    {

        if(minDistance < Vector3.Distance(transform.position, ball.transform.position))
        {
            movement.MyMovePositionWithoutY_Axis(ball.transform.position,10f); // speed must be linear with distance
        }
        else
        {
            movement.SetVelocity(Vector3.zero);
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
