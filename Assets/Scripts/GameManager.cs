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

}
