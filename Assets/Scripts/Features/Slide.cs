using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slide : MonoBehaviour
{

    [SerializeField]
    private float slidePower;

    private Ball ball;

    private Inputter inputter;
    private Rigidbody rb;


    private void Start()
    {

        ball = Ball.Instance;
        inputter = GetComponent<Inputter>();
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (inputter.GetButtonPassValue() > 0)
            Slide_(ball.transform.position, 0.3f);
    }

    private void Slide_(Vector3 targetPosition,float wait)
    {


        if (slideFinished && !ball.IsOwner(gameObject))
        {
            StartCoroutine(Slide__(targetPosition, slidePower, wait));
        }


    }





    private bool slideFinished = true;
    private IEnumerator Slide__(Vector3 targetPosition, float velocityMagnitude, float wait)
    {

        slideFinished = false;

        yield return new WaitForSeconds(wait);

        MyMovePosition(targetPosition, velocityMagnitude);

        slideFinished = true;

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
