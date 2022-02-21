using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deformation
{



    public static Vector3 Deform(Vector3 vector,float deformationMin, float deformationMax, float linearWith)
    {


        float deformation 
            = deformationMin + (deformationMax - deformationMin) * linearWith;
        float deformationX = Random.Range(-deformation, +deformation);
        float deformationY = Random.Range(-deformation, +deformation);
        float deformationZ = Random.Range(-deformation, +deformation);

        vector.x += deformationX;
        vector.y += deformationY;
        vector.z += deformationZ;

        return vector;

    }


    public static Vector2 Deform(Vector2 vector, float deformationMin, float deformationMax, float linearWith)
    {


        float deformation
            = deformationMin + (deformationMax - deformationMin) * linearWith;
        float deformationX = Random.Range(-deformation, +deformation);
        float deformationY = Random.Range(-deformation, +deformation);

        vector.x += deformationX;
        vector.y += deformationY;

        return vector;

    }

    public static float Deform(float val, float deformationMin, float deformationMax, float linearWith)
    {


        float deformation
            = deformationMin + (deformationMax - deformationMin) * linearWith;
        float deformationX = Random.Range(-deformation, +deformation);

        val += deformationX;

        return val;

    }


}
