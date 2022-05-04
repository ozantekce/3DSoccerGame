using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dribbling
{

    public static void Dribbling_(Dribblingable dribblingable, float verticalInput, float horizontalInput)
    {

        if (dribblingable.BallVision.NoControlBall()
            || dribblingable.DribblingCooldown.NotReady())
        {
            return;
        }
        

        GameObject gameObject = dribblingable.GameObject;
        float distanceWithBall = Vector3.Distance(Ball.Instance.transform.position, gameObject.transform.position);

        switch (distanceWithBall < dribblingable.MaxDistanceToDribbling)
        {
            case true:
                {

                    Ball.Instance.HitTheBall(CalculateHitVector(dribblingable, verticalInput, horizontalInput));

                }
                break;

        }
        float trackingSpeed= dribblingable.TrackingSpeed * CONSTANTS.Linear(distanceWithBall, 0, 3f);

        Movement.SpinToObject(dribblingable,Ball.Instance.gameObject,15f);
        Movement.MovePosition(dribblingable, Ball.Instance.transform.position,1.5f, trackingSpeed);


    }


    private static Vector3 CalculateHitVector(Dribblingable dribblingable,float verticalInput,float horizontalInput)
    {

        GameObject gameObject = dribblingable.GameObject;

        Vector3 ballForwardVector = (Ball.Instance.transform.position - gameObject.transform.position).normalized;

        Vector3 ballTargetForwardVector = new Vector3(verticalInput, 0, horizontalInput).normalized;

        float angle = Vector3.SignedAngle(ballForwardVector, ballTargetForwardVector, Vector3.up);
        if (Mathf.Abs(angle) > 160f)
        {
            ballTargetForwardVector = Quaternion.AngleAxis(-90, Vector3.up) * ballTargetForwardVector;
        }

        ballForwardVector.y = 0;

        float max = dribblingable.MovementSpeed + 5f;

        Vector3 rtn = dribblingable.DribblingPower * ballTargetForwardVector.normalized;

        if ((Ball.Instance.Rb.velocity + rtn).magnitude > max)
        {
            return rtn * 0.75f;
        }
        else
        {
            return rtn;
        }

    }


}
