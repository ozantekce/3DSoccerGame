using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GoalkeeperState
{
    void EnterTheState(Goalkeeper goalkeeper);
    void ExecuteTheState(Goalkeeper goalkeeper);

    void ExitTheState(Goalkeeper goalkeeper);


}
