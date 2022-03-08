using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Features/Dribbling")]
[RequireComponent(typeof(BallVision))]
public class Dribbling : MonoBehaviour
{

    private Ball ball;

    private Rigidbody rb;

    private Inputter inputter;
    private BallVision ballVision;

    private AnimationControl animationControl;

    [SerializeField]
    private float minDistanceWithBall, speed, hitPower, minDistanceToHit, delayToHit;

    private void Start()
    {

        ball = Ball.Instance;
        rb = GetComponent<Rigidbody>();
        inputter = GetComponent<Inputter>();
        ballVision = GetComponent<BallVision>();
        animationControl = GetComponent<AnimationControl>();
        cooldownForVertivalHit = new Cooldown(delayToHit);
        cooldownForHorizontalHit = new Cooldown(delayToHit);

    }


    private void Update()
    {

        if (ballVision.IsThereBallInVision())
        {
            HitTheBall();

            CloseDistanceWithBall();

            Spin();
            
        }


    }



    private Cooldown cooldownForVertivalHit;
    private Cooldown cooldownForHorizontalHit;
    private void HitTheBall()
    {
        float inputVertical = -inputter.GetJoyStickVerticalValueRaw();
        float inputHorizontal = inputter.GetJoyStickHorizontalValueRaw();
        float distanceWithBall = Vector3.Distance(transform.position, ball.transform.position);

        if (distanceWithBall <= minDistanceToHit)
        {
            if (inputVertical != 0 && cooldownForVertivalHit.Ready())
            {
                Vector3 directionVector = new Vector3(inputVertical, 0, 0).normalized;
                directionVector *= hitPower;
                directionVector.y = ball.Rb.velocity.y;
                directionVector.z = ball.Rb.velocity.z/2;
                ball.Rb.velocity = directionVector;
            }
            if (inputHorizontal != 0 && cooldownForHorizontalHit.Ready())
            {
                Vector3 directionVector = new Vector3(0, 0, inputHorizontal).normalized;
                directionVector *= hitPower;
                directionVector.x = ball.Rb.velocity.x / 2;
                directionVector.y = ball.Rb.velocity.y;
                ball.Rb.velocity = directionVector;
            }

        }


        Vector3 temp = ball.Rb.velocity;
        temp.y = 0;
        temp = Vector3.ClampMagnitude(temp, hitPower);
        temp.y = ball.Rb.velocity.y;

        ball.Rb.velocity = temp;
        

    }




    public void CloseDistanceWithBall()
    {

        Vector3 directionVector = ball.transform.position - transform.position;
        directionVector = directionVector.normalized;


        float tempSpeed = speed;
        float distanceWithBall = Vector3.Distance(transform.position, ball.transform.position);


        tempSpeed *= (distanceWithBall - minDistanceWithBall)*5;


        tempSpeed = Mathf.Clamp(tempSpeed, 0, speed);

        directionVector *= tempSpeed;
        directionVector.y = rb.velocity.y;

        rb.velocity = directionVector;

        if(directionVector.magnitude>2f)
            animationControl.ChangeAnimation("Run");

    }






    private float spinValue;
    private Vector3 eulerAng;
    public float spinSpeed = 500;
    public float slightGap = 10;//ignore low differences
    private void Spin()
    {

        Vector3 temp = transform.position - ball.transform.position;
        temp = temp.normalized;
        Vector2 angleVector = new Vector2();
        angleVector.x = -temp.z;
        angleVector.y = -temp.x;

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

    private Vector3 angles;

    private static float CalculateAngle(Vector2 vector)
    {
        if (vector == Vector2.zero) return -1;

        float angle = Mathf.Atan2(vector.y, vector.x);
        angle = Mathf.Rad2Deg * angle;
        if (angle < 0)
            angle += 360;

        return angle;
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

}
