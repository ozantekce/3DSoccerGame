using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Slideable : Movable
{

    bool IsSliding { get; set; }
    float SlidePower { get; set; }

    Cooldown SlideCooldown { get; }


}
