using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Fallable : Feature
{

    bool IsFalling { get; set; }

    bool FallCommand { get; set; }


}
