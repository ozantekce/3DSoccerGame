using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperCalculater
{


    public static Vector3 FindRequiredVelocity(Goalkeeper goalkeeper, Vector3 ballPosition, Vector3 ballVelocity)
    {

        Vector3 meetingPosition = FindMeetingPosition(goalkeeper, ballPosition, ballVelocity);

        float meetingTime = FindMeetingTime(goalkeeper, ballPosition, ballVelocity);

        Vector3 velocity = FindRequiredVelocity(goalkeeper, meetingPosition, meetingTime);


        if (
               float.IsNaN(velocity.x)
            || float.IsNaN(velocity.y)
            || float.IsNaN(velocity.z)
             )
        {
            return Vector3.zero;
        }
        else
        {
            
            return velocity;
        }


    }



    public static Vector3 FindRequiredVelocity(Goalkeeper goalkeeper, Vector3 meetingPosition, float meetingTime)
    {

        Vector3 catchAreaPosition = goalkeeper.CatchArea.transform.position;

        Vector3 velocity = (meetingPosition - catchAreaPosition) / meetingTime;
        
        
        Vector3 vel = Vector3.zero;

        vel.x = (meetingPosition.x - catchAreaPosition.x) / meetingTime;

        float distanceY = (meetingPosition.y-catchAreaPosition.y);

        vel.y = distanceY / meetingTime;
        vel.y += (meetingTime * (Gravity.GLOBAL_GRAVITY / 0.02f)) / 2f;


        //Debug.Log("old : " + velocity + " new : " + vel+" y : "+meetingPosition.y+" mt : " +meetingTime);
        return vel;

    }


    public static Vector3 FindMeetingPosition(Goalkeeper goalkeeper, Vector3 ballPosition,Vector3 ballVelocity)
    {

        Vector3 catchAreaPosition = goalkeeper.CatchArea.transform.position;

        float meetingTime = FindMeetingTime(goalkeeper, ballPosition, ballVelocity);

        Vector3 meetingPosition = ballPosition + ballVelocity * meetingTime;


        //Debug.Log("old : " + meetingPosition);

        meetingPosition.y -= (meetingTime * meetingTime * (Gravity.GLOBAL_GRAVITY/0.02f))/2f;

        if(meetingPosition.y < 1)
        {
            meetingPosition.y = 1;
        }
            


        return meetingPosition;

    }



    public static float FindMeetingTime(Goalkeeper goalkeeper, Vector3 ballPosition, Vector3 ballVelocity)
    {
        Vector3 catchAreaPosition = goalkeeper.CatchArea.transform.position;
        float meetingTime = (catchAreaPosition.z - ballPosition.z) / ballVelocity.z;
        return meetingTime;

    }







    public static float FindMeetingTime_(Vector3 goalkeeperPosition, Vector3 ballPosition, Vector3 ballVelocity)
    {

        
        float angle = Vector3.Angle(goalkeeperPosition, ballPosition);

        float distance = Mathf.Cos(angle)* Vector3.Distance(goalkeeperPosition,ballPosition);

        float t = distance / ballVelocity.magnitude;

        return t;


    }





}
