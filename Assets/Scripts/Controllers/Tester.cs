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

    private Dribbling dribbling;

    private Pass pass;

    private Slide slide;

    private Jump jump;



    void Start()
    {

        shootCooldown = new Cooldown(500f);
        movement = GetComponent<Movement>();
        inputter = GetComponent<Inputter>();
        shoot = GetComponent<Shoot>();
        ballVision = GetComponent<BallVision>();
        jump = GetComponent<Jump>();
        dribbling = GetComponent<Dribbling>();
        pass = GetComponent<Pass>();
        slide = GetComponent<Slide>();
        ball = Ball.Instance;

    }


    public Vector3 shootVector;
    void Update()
    {

        if (ball == null)
            ball = Ball.Instance;

        if (inputter == null)
            return;



        if(ball.owner == this.gameObject)
        {

            // v = 1 or -1 h = 0 -> x
            // v = 0       h = 1 or -1 -> z 
            // (v= 1  and h = 1) or (v = -1 and h =-1) -> xz
            // (v=-1 and h = 1) or (v=1 and h = -1) -> xz_
            // v=0 and h =0 -> 0


            float v = -inputter.GetJoyStickVerticalValueRaw();
            float h = inputter.GetJoyStickHorizontalValueRaw();


            if( (v==1 || v==-1) && h ==0)
                dribbling.Dribbling_(Axis.x, 14 * -inputter.GetJoyStickVerticalValueRaw(), Vector3.Distance(transform.position, ball.transform.position));
            else if ((h == 1 || h == -1) && v == 0)
                dribbling.Dribbling_(Axis.z, 14 * inputter.GetJoyStickHorizontalValueRaw(), Vector3.Distance(transform.position, ball.transform.position));
            else if((v == 1 && h == 1) || (v == -1 && h == -1))
                dribbling.Dribbling_(Axis.xz, 14 * -inputter.GetJoyStickVerticalValueRaw(), Vector3.Distance(transform.position, ball.transform.position));
            else if ((v == -1 && h == 1) ||(v == 1 && h == -1))
                dribbling.Dribbling_(Axis.xz_, 14 * -inputter.GetJoyStickHorizontalValueRaw(), Vector3.Distance(transform.position, ball.transform.position));
            else
            {
                dribbling.StopTheBall();
            }



            dribbling.CloseDistanceWithBall(2.2f,14f);




        }
        else
        {
            if (inputter.GetJoyStickVerticalValueRaw() != 0)
                SetVelocity(Axis.x, 15 * -inputter.GetJoyStickVerticalValue());
            if (inputter.GetJoyStickHorizontalValue() != 0)
                SetVelocity(Axis.z, 15 * inputter.GetJoyStickHorizontalValue());

            SpinToZXVector(new Vector2(inputter.GetJoyStickHorizontalValue(), -inputter.GetJoyStickVerticalValue()));


        }







        if (inputter.GetButtonShootValue() > 0 && shootCooldown.Ready())
        {
            ballVision.CooldownWaitToTakeBall.Reset();
            shootVector = new Vector3(0f, 0.7f, 0f);
            //print((transform.forward + shootVector));
            shoot.Shoot_(15*(transform.forward+shootVector)*inputter.GetButtonShootValue(),0.3f);
            //shoot.Shoot_(shootVector,0.3f);
        }

        if (inputter.GetButtonPassValue() > 0)
        {
            pass.Pass_(new Vector3(0,0,0),10f,0.3f);
        }

        if (inputter.GetButtonSlideValue() > 0)
        {
            slide.Slide_(ball.transform.position, 40f, 0.1f);
        }


        if (inputter.GetButtonJumpValue() > 0)
        {
            jump.Jump_(Vector3.up*1,0.3f);
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
