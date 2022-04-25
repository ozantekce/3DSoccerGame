using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchArea : MonoBehaviour
{

    public Transform rightHand;
    public Transform leftHand;
    [SerializeField]
    Goalkeeper goalkeeper;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ball"))
        {
            if (!processing&&goalkeeper.CurrentState==GoalkeeperJumpState.goalkeeperJumpState)
            {
                StartCoroutine(SendBallToHands());
            }

        }

    }


    private bool processing = false;

    IEnumerator SendBallToHands()
    {
        processing = true;
        //Debug.Log("start");
        Ball.Instance.Rb.velocity = Vector3.zero;
        Ball.Instance.Rb.angularVelocity = Vector3.zero;
        Ball.Instance.Rb.isKinematic = true;
        float T = 0;

        while (true)
        {

            T+=Time.deltaTime;
            float duration = 0.3f;
            float t01 = T/duration;

            Vector3 A = transform.position;
            Vector3 B = rightHand.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            Ball.Instance.transform.position = pos;

            if (t01 >= 1)
            {
                Ball.Instance.Rb.isKinematic = false;
                break;
            }

            yield return new WaitForEndOfFrame();

        }


        
        while (goalkeeper.CurrentState == GoalkeeperJumpState.goalkeeperJumpState)
        {

            yield return new WaitForEndOfFrame();

        }
        //Debug.Log("over");
        processing = false;

    }


}
