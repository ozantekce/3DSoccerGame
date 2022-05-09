using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTest : MonoBehaviour
{


    public Vector3 shotVector;

    private Rigidbody rigidbody;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }





    private bool ready =true;

    private bool running = false;

    public void Shot()
    {
        //Test_1();

        
        if (running)
            return;

        running = true;

        StartCoroutine(Testter());
        

    }



    private void Test_1()
    {

        shotVector.x = Random.value * 15f * (Random.value < 0.5f ? 1 : -1);
        shotVector.y = Random.value * 3f;
        shotVector.z = -35 - Random.value * 60f;
        Ball.Instance.Shot(shotVector);

    }



    private IEnumerator Testter()
    {

        while (true)
        {

            if (ready)
            {
                ready = false;

                shotVector.x = Random.value * 16f * (Random.value < 0.5f ? 1 : -1);
                shotVector.y = Random.value * 7f;
                shotVector.z = -35 - Random.value * 60f;
                Ball.Instance.Shot(shotVector);

                StartCoroutine(Resetter());

            }

            yield return new WaitForEndOfFrame();

        }


    }



    private IEnumerator Resetter()
    {
        

        yield return new WaitForSeconds(10f);

        rigidbody.velocity = Vector3.zero;
        transform.position = Vector3.zero;

        yield return new WaitForSeconds(3f);


        RandomPosition();

        yield return new WaitForSeconds(3f);

        ready =true;

        

    }


    private void RandomPosition()
    {

        float x;    //-20|20
        float z;    //-60|0

        x = Random.value*20f * (Random.value < 0.5f ? 1 : -1);
        z = Random.value * -63;

        Vector3 pos = new Vector3(x, 0, z);

        transform.position = pos;
        Debug.Log("ShotTest " + " Position reset "+ transform.position);

    }



}


