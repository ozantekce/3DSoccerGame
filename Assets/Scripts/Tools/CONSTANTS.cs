using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CONSTANTS 
{

    public readonly static Dictionary<Direction, Vector3> direction_Vector_Dictionary = Create_direction_Vector_Dictionary();
    private static Dictionary<Direction, Vector3> Create_direction_Vector_Dictionary()
    {
        Dictionary<Direction, Vector3> temp = new Dictionary<Direction, Vector3>();
        temp.Add(Direction.forward, Vector3.forward);
        temp.Add(Direction.backward, -Vector3.forward);
        temp.Add(Direction.left, -Vector3.right);
        temp.Add(Direction.right, Vector3.right);
        temp.Add(Direction.none, Vector3.zero);
        return temp;
    }



    public static float Linear(float value , float minValue , float maxValue)
    {
        if (value < minValue)
            return 0;
        if (value > maxValue)
            return 1;
        else
            return (1 / (maxValue - minValue)) * value;


    }
    



    public static float Normalize_angle_to_pos_neg_180(float angle)
    {

        float result_angle = angle;

        while (result_angle > 180)
            result_angle -= 2 * 180;
        while (result_angle < -180)
            result_angle += 2 * 180;

        return result_angle;


    }

}
