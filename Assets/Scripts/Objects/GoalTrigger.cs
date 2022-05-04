using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.Status != GameManager.GameStatus.running)
            return;

        if (other.CompareTag("Ball"))
        {
            Debug.Log("Goal");
            //GameManager.Instance.Goal(this);
        }

    }




}
