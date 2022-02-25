using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperCalculater
{



    public static Vector3 FindRequiredVelocity(Vector3 goalkeeperPosition, Vector3 ballPosition, Vector3 ballVelocity)
    {


        Vector3 velocity = FindRequiredVelocity(FindMeetingPosition(goalkeeperPosition, ballPosition, ballVelocity), goalkeeperPosition, FindMeetingTime(goalkeeperPosition, ballPosition, ballVelocity));
        
        if (
               float.IsNaN(velocity.x)
            || float.IsNaN(velocity.y)
            || float.IsNaN(velocity.z))
        {
            return Vector3.zero;
        }
        else
        {
            return velocity;
        }


    }



    private static Vector3 FindRequiredVelocity(Vector3 meetingPosition, Vector3 goalkeeperPoisiton, float meetingTime)
    {

        Vector3 velocity = (meetingPosition - goalkeeperPoisiton) / meetingTime;

        return velocity;
    }


    private static Vector3 FindMeetingPosition(Vector3 goalkeeperPosition,Vector3 ballPosition,Vector3 ballVelocity)
    {


        Vector3 meetingPosition =  ballPosition + ballVelocity * FindMeetingTime(goalkeeperPosition,ballPosition,ballVelocity);
        return meetingPosition;

    }


    private static float FindMeetingTime(Vector3 goalkeeperPosition, Vector3 ballPosition, Vector3 ballVelocity)
    {
        float angle = Vector3.Angle(goalkeeperPosition, ballPosition);

        float distance = Mathf.Cos(angle)* Vector3.Distance(goalkeeperPosition,ballPosition);

        float t = distance / ballVelocity.magnitude;

        return t;

        //return (goalkeeperPosition.z - ballPosition.z) / ballVelocity.z;
    }



}
