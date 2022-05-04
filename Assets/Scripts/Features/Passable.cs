using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Passable : BallControlable
{
    bool IsPassing { get; set; }
    float PassPower { get; set; }

    Cooldown PassCooldown { get; }

}

