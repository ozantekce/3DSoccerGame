using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass
{

    public static void Pass_(Passable passable, float passInput, Transform target)
    {


        GameObject gameObject = passable.GameObject;
        Vector3 directionVector = target.position - Ball.Instance.transform.position;
        directionVector = directionVector.normalized;
        Vector3 addVelocity
            = (passInput+0.4f) * passable.PassPower
            * (directionVector + new Vector3(0, 0.2f, 0)).normalized;

        if (VectorCalculater.CheckVectorXFrontOfVectorY(
                gameObject.transform.forward,
                (target.position - gameObject.transform.position).normalized
            ))
        {
            Ball.Instance.Rigidbody.velocity += addVelocity;
        }
        else
        {
            Ball.Instance.Rigidbody.velocity += gameObject.transform.forward * 15f;
        }
        
    }




}
