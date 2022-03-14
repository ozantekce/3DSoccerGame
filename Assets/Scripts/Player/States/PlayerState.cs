using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{


    void Enter(Player player);
    void Execute(Player player);

    void Exit(Player player);

    


}
