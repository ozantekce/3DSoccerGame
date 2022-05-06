using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : FiniteStateMachine
{


    private Player player;

    public Player Player { get => player; set => player = value; }



    public void Start()
    {
        player = GetComponent<Player>();
    }



}
