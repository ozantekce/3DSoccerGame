using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
public class Dribbling : MonoBehaviour
{

    private Ball ball;
    private Movement movement;


    private static float minDistanceToAddForce = 2.7f;


    private static float defaultMinDistance = 2.2f;

    [SerializeField]
    private float cooldownDribbling = 300f;

    private CooldownManualReset cooldownToDribbling;

    private void Start()
    {
        ball = Ball.Instance;
        movement = GetComponent<Movement>();

        cooldownToDribbling = new CooldownManualReset(cooldownDribbling);

    }


    private void Update()
    {


        if(ball.IsOwner(gameObject))
        {

            SpinForBall();

        }


    }


    private void SpinForBall()
    {

        Vector3 temp = transform.position - ball.transform.position;
        temp = temp.normalized;
        Vector2 angleVector = new Vector2();
        angleVector.x = temp.z;
        angleVector.y = temp.x;
        movement.SpinToZXVector(-angleVector);

    }


    
    public void GiveForceTheBall(Vector3 vector3, float distanceWithOwner)
    {
        if (cooldownToDribbling.TimeOver())
        {

            if (distanceWithOwner < minDistanceToAddForce)
            {
                ball.Movement.GiveForce(vector3);
                
                if(ball.GetVelocity().magnitude < 16f)
                    ball.Movement.GiveVelocity(vector3.normalized*4f);
                
                cooldownToDribbling.ResetTimer();
            }
                

        }

    }

    public void SlowDownTheBall()
    {
        if (ball.Movement.GetVelocity().sqrMagnitude < 4f)
            return;

        if(Time.frameCount % 5 == 0)
            ball.Movement.SetVelocityWithoutY(ball.Movement.GetVelocity() / 1.2f);


    }

    public void CloseDistanceWithBall(float minDistance,float speed)
    {

        if (minDistance < Vector3.Distance(transform.position, ball.transform.position))
        {
            movement.MyMovePositionWithoutY_Axis(ball.transform.position, speed); // speed must be linear with distance
        }
        else
        {
            movement.SetVelocityWithoutY(Vector3.zero);
        }

    }

    
    public void CloseDistanceWithBall(float speed)
    {

        if (defaultMinDistance < Vector3.Distance(transform.position, ball.transform.position))
        {
            movement.MyMovePositionWithoutY_Axis(ball.transform.position, speed); // speed must be linear with distance
        }
        else
        {
            movement.SetVelocityWithoutY(Vector3.zero);
        }

    }


}
