using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTest : MonoBehaviour
{


    public Vector3 shotVector;




    private void Start()
    {


    }



    public void Shot()
    {
        shotVector.x = Random.value * 12f * (Random.value<0.5f? 1:-1) ;
        shotVector.y = Random.value*12f;
        shotVector.z = -35-Random.value*40f;
        Ball.Instance.Shot(shotVector);

    }

}


