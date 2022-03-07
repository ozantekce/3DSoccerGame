using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dribbling : MonoBehaviour
{

    private Ball ball;

    private Rigidbody rb;

    private Inputter inputter;

    [SerializeField]
    private float minDistanceWithBall, speed , hitPower;

    private void Start()
    {

        ball = Ball.Instance;
        rb = GetComponent<Rigidbody>();
        cooldownHit = new CooldownManualReset(cdHit);
        inputter = GetComponent<Inputter>();

    }


    private void Update()
    {

        if (ball.IsOwner(this.gameObject))
        {
            HitTheBall();

            CloseDistanceWithBall();

            Spin();
        }


    }


    [SerializeField]
    private float minDistanceToHit =2.5f;
    private float cdHit = 200f;
    private CooldownManualReset cooldownHit;
    public void HitTheBall()
    {
        float distanceWithBall = Vector3.Distance(transform.position, ball.transform.position);

        if ( distanceWithBall <= minDistanceToHit && cooldownHit.TimeOver())
        {
            float inputVertical = -inputter.GetJoyStickVerticalValueRaw();
            float inputHorizontal = inputter.GetJoyStickHorizontalValueRaw();
            Vector3 directionVector = new Vector3(inputVertical, 0, inputHorizontal).normalized;

            ball.Rb.velocity = directionVector * hitPower;
            cooldownHit.ResetTimer();
        }

    }


    public void CloseDistanceWithBall()
    {
        float inputVertical = -inputter.GetJoyStickVerticalValueRaw();
        float inputHorizontal = inputter.GetJoyStickHorizontalValueRaw();
        if (inputHorizontal == 0 && inputVertical == 0)
            return;
        if (minDistanceWithBall < Vector3.Distance(transform.position, ball.transform.position))
        {
            MyMovePosition(ball.transform.position, speed); // speed must be linear with distance
        }


    }


    public void MyMovePosition(Vector3 position, float speed)
    {

        if (transform.position != position)
        {
            Vector3 directionVector = position - transform.position;
            directionVector = directionVector.normalized;
            directionVector.y = 0;

            speed *= (Vector3.Distance(position, transform.position)-minDistanceWithBall);

            if (speed < 0)
                return;

            rb.velocity = directionVector * speed;
        }


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
