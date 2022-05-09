using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperConditionMethods : MonoBehaviour
{





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




}
