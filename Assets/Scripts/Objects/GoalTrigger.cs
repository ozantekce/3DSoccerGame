using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ball"))
        {
            Debug.Log("Goal");
            GameFlowController.Instance.Goal(this);
        }

    }




}
