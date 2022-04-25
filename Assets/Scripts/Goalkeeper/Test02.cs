using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test02 : MonoBehaviour
{

    public Transform center;
    private float radius = 12f;
    private Ball ball;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (ball == null)
            ball = Ball.Instance;

        Vector3 temp = (ball.transform.position - center.transform.position).normalized;

        Vector3 target = center.position + temp * radius;

        if(center.position.z > target.z)
            target.z = center.position.z;

        MyMovePosition(target, 5f);


    }


    public void MyMovePosition(Vector3 position, float speed)
    {

        if (transform.position != position)
        {
            Vector3 directionVector = position - transform.position;
            directionVector = directionVector.normalized;
            directionVector.y = 0;
            speed = Mathf.Clamp(speed, 0, Vector3.Distance(position, transform.position));
            rb.velocity = directionVector * speed;
        }


    }


}
