using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    private bool holdTheBall;

    private bool hasBall;

    [SerializeField]
    private bool dropBall;



    public bool HasBall { get => hasBall; set => hasBall = value; }
    public bool DropBall { get => dropBall; set => dropBall = value; }
    public bool HoldTheBall { get => holdTheBall; set => holdTheBall = value; }

    // Start is called before the first frame update
    void Start()
    {
        dropBall = true;
        holdTheBall = false;
    }



    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Ball"))
        {
            // Debug.Log("ball ");
            
            if (!dropBall)
            {
                Ball.Instance.Rb.velocity = Vector3.zero;
                Ball.Instance.Rb.angularVelocity = Vector3.zero;
                Ball.Instance.Rb.MovePosition(transform.position);

                //Ball.Instance.transform.position = transform.position;

            }
                

            hasBall = true;
            
        }

    }


    private void OnTriggerExit(Collider other)
    {

        hasBall = false;

    }



}
