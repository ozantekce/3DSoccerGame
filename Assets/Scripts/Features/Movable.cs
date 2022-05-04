using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Movable : Feature
{


    float MovementSpeed { get; set; }

    float SpinSpeed { get; set; }

    Rigidbody Rigidbody { get; set; }


}
