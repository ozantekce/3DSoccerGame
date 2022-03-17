using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{


    void EnterTheState(Player player);
    void ExecuteTheState(Player player);

    void ExitTheState(Player player);

    


}
