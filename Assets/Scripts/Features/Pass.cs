using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : MonoBehaviour
{
    public Ball ball;



    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;
    }


    public void Pass_(Vector3 targetPosition, float velocityMagnitude, float wait)
    {

        if (passFinished && ball.Owner == this.gameObject)
        {
            StartCoroutine(Pass__(targetPosition, velocityMagnitude, wait));
        }

    }


    private bool passFinished = true;
    private IEnumerator Pass__(Vector3 targetPosition, float velocityMagnitude, float wait)
    {

        passFinished = false;

        yield return new WaitForSeconds(wait);
        ball.owner = null;
        ball.Movement.MyMovePosition(targetPosition, velocityMagnitude);

        passFinished = true;

    }





}
