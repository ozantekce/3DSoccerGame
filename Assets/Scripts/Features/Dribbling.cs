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


    public void Dribbling_(Axis axis, float value, float distanceWithOwner)
    {

        ball.Dribbling(axis, value, distanceWithOwner);

    }

    public void CloseDistanceWithBall(float minDistance)
    {

        if (minDistance < Vector3.Distance(transform.position, ball.transform.position))
        {
            movement.MyMovePositionWithoutY_Axis(ball.transform.position, 10f); // speed must be linear with distance
        }
        else
        {
            movement.SetVelocity(Vector3.zero);
        }

    }


}
