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
    public void Dribbling_(Vector3 vector3, float distanceWithOwner)
    {
        if (cooldownToDribbling.Ready())
        {
            if (distanceWithOwner < 2.7f)
                ball.Movement.GiveForce(vector3);

        }

    }

    public void StopTheBall()
    {

        ball.Movement.SetVelocityWithoutY(Vector3.zero);
        movement.SetVelocity(Vector3.zero);
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
