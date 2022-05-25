using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperConditionMethods : MonoBehaviour
{


    public static bool BallShoted(FiniteStateMachine fsm)
    {

        return Ball.Instance.IsShoted;

    }

    public static bool MeetingWithBall(FiniteStateMachine fsm)
    {

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Vector3 meetingPosition
            = GoalkeeperCalculater.CalculateAll_(goalkeeper)[0];

        return goalkeeper.Other.IntersectWithMeetingPosition(meetingPosition);

    }

    public static bool BallShotedAndMeetingWithThis(FiniteStateMachine fsm)
    {

        return BallShoted(fsm) && MeetingWithBall(fsm);

    }


    public static bool RightPosition(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        return goalkeeper.RightPosition();

    }

    public static bool WrongPosition(FiniteStateMachine fsm)
    {

        return !RightPosition(fsm);

    }

    public static bool BallSoClose(FiniteStateMachine fsm)
    {
        Transform transform = fsm.transform;

        float distance
            = Vector3.Distance(transform.position, Ball.Instance.transform.position);

        return distance < 15f;

    }


    public static bool BallCatchable(FiniteStateMachine fsm)
    {

        if (Ball.Instance.Rigidbody.isKinematic)
            return false;

        Collider[] intersecting 
            = Physics.OverlapSphere(Ball.Instance.transform.position, 2f);

        GameObject catchArea
            = ((GoalkeeperFSM)fsm).Goalkeeper.CatchArea;

        foreach (Collider c in intersecting)
        {
            if (c.gameObject == catchArea)
            {
                return true;
            }

        }

        return false;
    }



    /// <summary>
    /// Ball so close
    /// </summary>
    /// <param name="fsm"></param>
    /// <returns></returns>
    public static bool BallGrabbable(FiniteStateMachine fsm)
    {
        Transform transform = fsm.transform;

        float distance
            = Vector3.Distance(transform.position, Ball.Instance.transform.position);

        return distance < 1.3f;
    }


    public static bool GoalpostFar(FiniteStateMachine fsm)
    {

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Transform goalpost = goalkeeper.GoalpostCenter;

        float distance
            = Vector3.Distance(goalkeeper.transform.position, goalpost.position);

        return distance > 30f;

    }


    /// <summary>
    /// That means physics engine stopped for an animation
    /// </summary>
    /// <param name="fsm"></param>
    /// <returns></returns>
    public static bool BallIsKinematic(FiniteStateMachine fsm)
    {

        return Ball.Instance.Rigidbody.isKinematic;

    }

    public static bool NotBallIsKinematic(FiniteStateMachine fsm)
    {

        return !Ball.Instance.Rigidbody.isKinematic;

    }



    /// <summary>
    /// That means the ball is in the hands of the goalkeeper
    /// </summary>
    /// <returns></returns>
    public static bool BallIsTrigger(FiniteStateMachine fsm)
    {

        return Ball.Instance.Collider.isTrigger;

    }

    public static bool NotBallIsTrigger(FiniteStateMachine fsm)
    {

        return !BallIsTrigger(fsm);

    }


    public static bool Elapsed4Seconds(FiniteStateMachine fsm)
    {

        if (fsm.ElapsedTimeInCurrentState() > 4f)
            return true;


        return false;
    }



    public static bool Elapsed2Seconds(FiniteStateMachine fsm)
    {

        if (fsm.ElapsedTimeInCurrentState() > 2f)
            return true;


        return false;
    }



}
