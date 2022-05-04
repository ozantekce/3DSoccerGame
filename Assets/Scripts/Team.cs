using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{

    public enum TeamNameEnum
    {
        blue,red
    }
    [SerializeField]
    private TeamNameEnum teamName;

    public TeamNameEnum TeamName { get => teamName; set => teamName = value; }

}
