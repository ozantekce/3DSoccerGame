using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Ball ball;


    //--
    [SerializeField]
    private float movementSpeed = 15f, shootPower = 15f , passPower = 30f, slidePower = 50f, dribblingSpeed = 14f , spinSpeed =500f ;

    //--


    //--

    private Movement movement;
    private Dribbling dribbling;
    private Pass pass;
    private Slide slide;
    private Shoot shoot;
    private Inputter inputter;
    private BallVision ballVision;

    //--


    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;

        movement = GetComponent<Movement>();
        inputter = GetComponent<Inputter>();
        shoot = GetComponent<Shoot>();
        ballVision = GetComponent<BallVision>();
        dribbling = GetComponent<Dribbling>();
        pass = GetComponent<Pass>();
        slide = GetComponent<Slide>();


    }


    // inputs 
    private float inputVertical;
    private float inputHorizontal;
    private float inputShoot;
    private float inputPass;
    private float inputSlide;

    void Update()
    {


        ReadInputs();
        ExecuteInputs();




    }


    private void ReadInputs()
    {
        if (inputter == null)
            return;

        inputVertical = -inputter.GetJoyStickVerticalValueRaw();
        inputHorizontal = inputter.GetJoyStickHorizontalValueRaw();

        inputShoot = inputter.GetButtonShootValue();
        inputPass = inputter.GetButtonPassValue();
        inputSlide = inputter.GetButtonSlideValue();


    }


    private void ExecuteInputs()
    {


        Movement();

        if (inputter.GetButtonShootValue() > 0)
            Shoot();
        if (inputter.GetButtonPassValue() > 0)
            Pass();
        if (inputter.GetButtonSlideValue() > 0)
            Slide();



    }

    private void Movement()
    {
        if (ball.IsOwner(this.gameObject))
        {
            Dribbling();
        }
        else
        {

            movement.SetSpecificAxisVelocity(Axis.x, movementSpeed * inputVertical);
            movement.SetSpecificAxisVelocity(Axis.z, movementSpeed * inputHorizontal);
            Spin();

        }
    }

    private void Dribbling()
    {

        if (inputVertical == 0 && inputHorizontal == 0)
        {
            dribbling.SlowDownTheBall();
            dribbling.CloseDistanceWithBall(dribblingSpeed);
        }
        else
        {
            Vector3 vector3 = Vector3.right * inputVertical;
            vector3 += Vector3.forward * inputHorizontal;


            dribbling.GiveForceTheBall(vector3 * (dribblingSpeed*5.5f), Vector3.Distance(transform.position, ball.transform.position));
            dribbling.CloseDistanceWithBall(dribblingSpeed);

        }

    }

    private void Shoot()
    {

        shoot.Shoot_(inputShoot * shootPower * (transform.forward + new Vector3(0,0.4f,0)));

    }

    private void Slide()
    {

        slide.Slide_(ball.transform.position, slidePower*inputSlide);

    }

    private void Pass()
    {
        pass.Pass_(GetCloserTeammate(), passPower*inputPass);
    }



    // 2 vs 2 oyun için direk diðer oyuncu  

    public Transform teammate;
    private Vector3 GetCloserTeammate()
    {
        return teammate.position + teammate.forward*inputPass;
    }






    private float slightGap = 10;//ignore low differences


    private float spinValue;
    private Vector3 eulerAng;
    // we can use here angular velocity
    private void Spin()
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

        movement.Spin(spinValue, Axis.y);

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
