using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Shotable : BallControlable
{

    float ShotPower { get; set; }

    Cooldown ShotCooldown { get; }


}
