using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    //Singleton
    private static GameManager instance = null;

    public static GameManager Instance
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


    public List<SpawnableGameObject> handler;



    // Start is called before the first frame update
    void Start()
    {
        
        handler = new List<SpawnableGameObject>();

        Resources.ObjectsInfo[] objectsInfo  = Resources.Instance.objectsArray;

        for (int i = 0; i < objectsInfo.Length; i++)
        {
            handler.Add(new SpawnableGameObject(objectsInfo[i].name, objectsInfo[i].prefab
                , objectsInfo[i].startPosition,objectsInfo[i].angles));

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private void ResetAllPositions()
    {

        for(int i = 0;i < handler.Count; i++)
        {
            handler[i].ResetPosition();
        }

    }

    private void ResetPosition(string name)
    {

        for (int i = 0; i < handler.Count; i++)
        {
            if(handler[i].Name == name) {
                handler[i].ResetPosition();
                return;
            }
        }

    }







}
