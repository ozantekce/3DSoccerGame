using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorCalculater
{



    public static float FindAngleAroundVectors(Vector3 vector1,Vector3 vector2)
    {

        return Vector3.Angle(vector1,vector2);

    }


    public static bool CheckVectorXFrontOfVectorY(Vector3 vectorX,Vector3 vectorY)
    {

        float cos = (Vector3.Dot(vectorX, vectorY) / (vectorX.magnitude* vectorY.magnitude));
        Debug.Log(cos);
        return cos > 0;

    }


    public static Vector3 PreventToPassMaxMagnitude(Vector3 vector3, float max)
    {

        if (vector3.magnitude > max)
            vector3 = vector3.normalized * max;
        return vector3;

    }


    public static Vector3 CalculateDirectionVector(Vector3 position, Vector3 target)
    {
        return ( target - position ).normalized;
    }

    public static Vector3 CalculateDirectionVectorWithoutYAxis(Vector3 position, Vector3 target)
    {

        target.y = 0;
        position.y = 0;

        return (target - position).normalized;

    }


    public static Vector2 Vector3toVector2(Vector3 vector, Axis removeAxis)
    {
        Vector2 rtn = Vector2.zero;
        if(removeAxis == Axis.x)
        {
            rtn.x = vector.y;
            rtn.y = vector.z;
        }else if(removeAxis == Axis.y)
        {
            rtn.x = vector.x;
            rtn.y = vector.z;
        }else if(removeAxis == Axis.z)
        {
            rtn.x = vector.x;
            rtn.y= vector.y;
        }

        return rtn;

    }


}
