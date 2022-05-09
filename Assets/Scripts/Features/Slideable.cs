using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Slideable : Movable
{

    float SlidePower { get; set; }

    Cooldown SlideCooldown { get; }


}
