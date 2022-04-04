using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private GameStatus lastStatus;
    private GameStatus status;
    public enum GameStatus
    {

        opening, running, stopped, ending

    }



    private int teamOneScore;
    private int teamTwoScore;


    //Singleton
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameStatus Status { get => status;}
    public GameStatus LastStatus { get => lastStatus; }

    private void Awake()
    {
        instance = this;
        SetStatus(GameStatus.opening);
        Application.targetFrameRate = 60;
    }


    public List<SpawnableGameObject> allList;

    public List<SpawnableGameObject> teamOneList;
    public List<SpawnableGameObject> teamTwoList;
    public List<SpawnableGameObject> othersList;


    public Camera camera;
    private CinemachineVirtualCamera virtualCamera;


    // Start is called before the first frame update
    void Start()
    {


        allList = new List<SpawnableGameObject>();
        teamOneList = new List<SpawnableGameObject>();
        teamTwoList = new List<SpawnableGameObject>();
        othersList = new List<SpawnableGameObject>();



        Resources.ObjectsInfo[] teamOneArray  = Resources.Instance.teamOneArray;
        Resources.ObjectsInfo[] teamTwoArray = Resources.Instance.teamTwoArray;
        Resources.ObjectsInfo[] othersArray = Resources.Instance.othersArray;



        for (int i = 0; i < teamOneArray.Length; i++)
        {
            teamOneList.Add(new SpawnableGameObject(teamOneArray[i].name, teamOneArray[i].prefab
                , teamOneArray[i].startPosition, teamOneArray[i].angles));
            allList.Add(teamOneList[i]);
            teamOneList[i].GameObject_.GetComponent<Player>().Team = 1;
            teamOneList[i].GameObject_.GetComponent<Player>().PlayerIndex = (i+1);

        }

        for (int i = 0; i < teamTwoArray.Length; i++)
        {
            teamTwoList.Add(new SpawnableGameObject(teamTwoArray[i].name, teamTwoArray[i].prefab
                , teamTwoArray[i].startPosition, teamTwoArray[i].angles));
            allList.Add(teamTwoList[i]);
            teamTwoList[i].GameObject_.GetComponent<Player>().Team = 2;
            teamTwoList[i].GameObject_.GetComponent<Player>().PlayerIndex = (i + 1);
        }


        for (int i = 0; i < othersArray.Length; i++)
        {
            othersList.Add(new SpawnableGameObject(othersArray[i].name, othersArray[i].prefab
                , othersArray[i].startPosition, othersArray[i].angles));
            allList.Add(othersList[i]);
        }


        virtualCamera = camera.GetComponentInChildren<CinemachineVirtualCamera>();
        if (virtualCamera.LookAt == null)
        {

            virtualCamera.LookAt = Ball.Instance.transform;
            virtualCamera.Follow = Ball.Instance.transform;

        }


        SetStatus(GameStatus.running);

        
    }





    public void ResetAllPositions()
    {

        for(int i = 0;i < allList.Count; i++)
        {
            allList[i].ResetPosition();
        }

    }

    public void ResetPosition(string name)
    {

        for (int i = 0; i < allList.Count; i++)
        {
            if(allList[i].Name == name) {
                allList[i].ResetPosition();
                return;
            }
        }

    }




    public void Goal(GoalTrigger goalTrigger)
    {
        SetStatus(GameStatus.stopped);

        if (goalTrigger.CompareTag("GoalTriggerOne"))
            teamOneScore++;
        else if (goalTrigger.CompareTag("GoalTriggerTwo"))
            teamTwoScore++;

        StartCoroutine(Goal_());

    }

    private IEnumerator Goal_()
    {

        //geçiþ eklenebilir

        yield return new WaitForSeconds(5f);


        ResetAllPositions();


        yield return new WaitForSeconds(5f);

        SetStatus(GameStatus.running);

    }


    public void SetStatus(GameStatus status)
    {

        lastStatus = this.status;
        this.status = status;
        print(status);

    }








}
