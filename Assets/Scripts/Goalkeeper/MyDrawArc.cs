using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyDrawArc : MonoBehaviour
{
    public float shieldArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Create a 180 degrees wire arc with a ScaleValueHandle attached to the disc
    // that lets you modify the "shieldArea" value in the WireArcExample
    [CustomEditor(typeof(MyDrawArc))]
    public class DrawWireArc : Editor
    {
        void OnSceneGUI()
        {
            Handles.color = Color.red;
            MyDrawArc myObj = (MyDrawArc)target;
            Handles.DrawWireArc(myObj.transform.position, myObj.transform.up, -myObj.transform.right, 180, myObj.shieldArea);
            myObj.shieldArea = (float)Handles.ScaleValueHandle(myObj.shieldArea, myObj.transform.position + myObj.transform.forward * myObj.shieldArea, myObj.transform.rotation, 1, Handles.ConeHandleCap, 1);
        }
    }

}
