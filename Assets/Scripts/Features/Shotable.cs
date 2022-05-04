using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Shotable : BallControlable
{

    bool IsShooting { get; set; }
    float ShotPower { get; set; }

    Cooldown ShotCooldown { get; }


}
