using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{






    //Singleton
    private static GameFlowController instance = null;

    public static GameFlowController Instance
    {
        get
        {
            return instance;
        }
    }


    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }





    private bool gameRunning;
    private bool canGoal;



    private void Update()
    {
        

        

    }




    public void Goal(GoalTrigger goalTrigger)
    {

        Debug.Log(goalTrigger.name);
        GameManager.Instance.ResetAllPositions();
    }





}
