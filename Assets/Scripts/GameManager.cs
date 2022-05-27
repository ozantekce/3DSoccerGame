using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private GameStatus status;

    //Singleton
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameStatus Status { get => status; }

    private void Awake()
    {
        instance = this;
        SetStatus(GameStatus.opening);
        Application.targetFrameRate = 60;

    }


    private void Start()
    {
        SetStatus(GameStatus.running);
    }

    public enum GameStatus
    {

        opening, running, stopped, ending

    }

    public void SetStatus(GameStatus status)
    {
        this.status = status;
        print(status);

    }


    public void Goal(GoalTrigger goalTrigger)
    {
        Footballer [] footballers = FindObjectsOfType<Footballer>();
        for (int i = 0; i < footballers.Length; i++)
        {

            footballers[i].transform.position = Vector3.zero;
        }
        Ball.Instance.transform.position = Vector3.zero;

    }

}
