using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
public class Dribbling : MonoBehaviour
{

    private Ball ball;
    private Movement movement;

    [SerializeField]
    private float minDistanceToAddForce = 2.7f;

    [SerializeField]
    private float cooldownDribbling = 100f;

    private Cooldown cooldownToDribbling;

    private void Start()
    {
        ball = Ball.Instance;
        movement = GetComponent<Movement>();

        cooldownToDribbling = new Cooldown(cooldownDribbling);

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


    
    public void Dribbling_(Vector3 vector3, float distanceWithOwner)
    {
        if (cooldownToDribbling.Ready())
        {
            if (distanceWithOwner < minDistanceToAddForce)
            {
                ball.Movement.GiveForce(vector3);
                 ball.Movement.GiveVelocity(vector3.normalized);
            }
                

        }

    }

    public void StopTheBall()
    {

        if(Vector3.Distance(transform.position, ball.transform.position) <= minDistanceToAddForce)
            ball.Movement.SetVelocityWithoutY(Vector3.zero);
        else if(Time.frameCount % 5 == 0)
            ball.Movement.SetVelocityWithoutY(ball.Movement.GetVelocity() / 1.2f);

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
