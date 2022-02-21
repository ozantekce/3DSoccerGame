using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CONSTANTS 
{

    public readonly static Dictionary<Directions, Vector3> direction_Vector_Dictionary = Create_direction_Vector_Dictionary();
    private static Dictionary<Directions, Vector3> Create_direction_Vector_Dictionary()
    {
        Dictionary<Directions, Vector3> temp = new Dictionary<Directions, Vector3>();
        temp.Add(Directions.forward, Vector3.forward);
        temp.Add(Directions.backward, -Vector3.forward);
        temp.Add(Directions.left, -Vector3.right);
        temp.Add(Directions.right, Vector3.right);
        temp.Add(Directions.none, Vector3.zero);
        return temp;
    }


}
