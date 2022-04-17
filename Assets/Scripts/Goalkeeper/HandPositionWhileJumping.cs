using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPositionWhileJumping : MonoBehaviour
{

    public Transform rightHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ball"))
        {
            //Debug.Log("ball ");
            //Ball.Instance.transform.position = rightHand.transform.position;
            Ball.Instance.Rb.MovePosition(rightHand.transform.position);

        }

    }



}
