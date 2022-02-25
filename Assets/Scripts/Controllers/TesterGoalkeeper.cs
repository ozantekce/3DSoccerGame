using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterGoalkeeper : MonoBehaviour
{


    public Ball ball;
    public float maxVx;
    public float maxVy;





    private float T;
    private float T1,T2;


    private Vector3 meetingPosition;

    private float goalkeeperVx;
    private float goalkeeperVy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == null)
            ball = Ball.Instance;
        CalculateMeetingPosition();
    }



    private void CalculateMeetingPosition()
    {


        float ballVx = ball.GetVelocity().x;
        float ballVy = ball.GetVelocity().y;
        float ballVz = ball.GetVelocity().z;


        
        T = CalculateT(CalculateDistance(),ballVz);

        meetingPosition.x = T * ballVx + ball.transform.position.x;
        meetingPosition.y = T * ballVy + ball.transform.position.y;
        meetingPosition.z = T * ballVz + ball.transform.position.z;



    }



    private float CalculateT(float distanceZ, float Vz)
    {

        return distanceZ / Vz;

    }


    private float CalculateDistance()
    {

        return Vector3.Distance(this.transform.position, ball.transform.position);

    }



}
