using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Passable : BallControlable
{
    float PassPower { get; set; }

    Cooldown PassCooldown { get; }

}

