using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
public class Slide : MonoBehaviour
{

    private Ball ball;
    private Movement movement;



    private void Start()
    {
        ball = Ball.Instance;
        movement = GetComponent<Movement>();
    }

    public void Slide_(Vector3 targetPosition, float velocityMagnitude, float wait)
    {


        if (slideFinished && !ball.IsOwner(gameObject))
        {
            StartCoroutine(Slide__(targetPosition, velocityMagnitude, wait));
        }


    }


    private bool slideFinished = true;
    private IEnumerator Slide__(Vector3 targetPosition, float velocityMagnitude, float wait)
    {

        slideFinished = false;

        yield return new WaitForSeconds(wait);

        movement.MyMovePositionWithoutY_Axis(targetPosition, velocityMagnitude);

        slideFinished = true;

    }


}
