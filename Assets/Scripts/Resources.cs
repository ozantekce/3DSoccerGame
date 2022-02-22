using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{


    //Singleton
    private static Resources instance = null;

    public static Resources Instance
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


    [Serializable]
    public struct ObjectsInfo
    {
        public string name;
        public GameObject prefab;
        public Vector3 startPosition;
        public Quaternion angles;

    };


    public ObjectsInfo[] objectsArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
