using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorCalculater
{



    public static float FindAngleAroundVectors(Vector3 vector1,Vector3 vector2)
    {

        return Vector3.Angle(vector1,vector2);

    }



    public static bool CheckVector2FrontOfVector1(Vector3 vector1,Vector3 vector2)
    {

        float cos = (Vector3.Dot(vector1,vector2) / (vector1.magnitude*vector2.magnitude));

        return cos > 0;

    }








}
