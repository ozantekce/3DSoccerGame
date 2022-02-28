using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
public class Dribbling : MonoBehaviour
{

    private Ball ball;
    private Movement movement;

    private void Start()
    {
        ball = Ball.Instance;
        movement = GetComponent<Movement>();

        cooldownToDribbling = new Cooldown(100f);

    }


    private void Update()
    {


        if(ball.owner == this.gameObject)
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


    private Cooldown cooldownToDribbling;
    private Axis lastAxis = Axis.y;
    public void Dribbling_(Axis axis, float value, float distanceWithOwner)
    {
        if (cooldownToDribbling.Ready())
        {
            if (lastAxis == axis)
            {
                if (distanceWithOwner <= 2.7f)
                {
                    ball.Movement.SetVelocityWithoutY(movement.GetVelocity() / 2);
                    ball.Movement.SetSpecificAxisVelocity(axis, value);
                    lastAxis = axis;
                }

            }
            else
            {
                ball.Movement.SetVelocityWithoutY(Vector3.zero);
                ball.Movement.SetSpecificAxisVelocity(axis, value / 2);
                lastAxis = axis;
            }
        }

    }


    public void StopTheBall()
    {

        ball.Movement.SetVelocityWithoutY(Vector3.zero);
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


}
