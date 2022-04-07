using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour
{

    private Goalkeeper goalkeeper;

    // Start is called before the first frame update
    void Start()
    {
        goalkeeper = GetComponent<Goalkeeper>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Spin(goalkeeper);

    }

    private float gap = 4f;        //  küçük açý farklarý görmezden gelinir
    private float spinSpeed = 500f; //  rotation deðiþim hýzý
    private void Spin(Goalkeeper goalkeeper)
    {
        Vector3 ballForwardVector = (goalkeeper.Ball.transform.position - goalkeeper.transform.position).normalized;
        Vector2 targetForward = new Vector3(ballForwardVector.x, ballForwardVector.z).normalized;


        Vector2 curretForward = new Vector2(goalkeeper.transform.forward.x, goalkeeper.transform.forward.z);

        //  aradaki açý bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki açý küçük deðil ise iþlem yapýlýr
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- dönme yönüne göre playerýn eulerAnglesý spinSpeed*Time.deltaTime kadar deðiþtirildi
            Vector3 eulerAng = goalkeeper.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1 : -1);
            goalkeeper.transform.eulerAngles = eulerAng;
            //---

        }

    }


}
