using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableGameObject
{

    private string name; 
    private GameObject gameObject_;
    private GameObject prefab;
    private Vector3 startPosition;
    private Quaternion startRotation;

    public SpawnableGameObject(string name, GameObject prefab
        , Vector3 startPosition, Quaternion startRotation)
    {
        this.name = name;
        this.prefab = prefab;
        this.startPosition = startPosition;
        this.startRotation = startRotation;
        CreateGameObject();
    }


    public Quaternion StartRotation { get => startRotation; set => startRotation = value; }
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public string Name { get => name; set { 
            name = value;
            GameObject_.name = name;
        } 
    }
    public GameObject GameObject_ { get => gameObject_; set => gameObject_ = value; }
    public GameObject Prefab { get => prefab; set => prefab = value; }

    private void CreateGameObject() {

        GameObject_ = GameObject.Instantiate(prefab,startPosition,startRotation);
        GameObject_.name = name;
        
    }


    public void ResetPosition()
    {
        GameObject_.transform.position = startPosition;
        GameObject_.transform.rotation = startRotation;
    }




}
