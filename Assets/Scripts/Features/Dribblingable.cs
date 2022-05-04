using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Dribblingable : Movable,BallControlable
{


    float MaxDistanceToDribbling { get; set; }
    float DribblingPower { get; set; }

    float TrackingSpeed { get; set; }

    Cooldown DribblingCooldown { get; }


}
