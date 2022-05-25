using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Jumpable : Movable
{

    float JumpPowerY { get; set; }
    float JumpPowerX { get; set; }


    Cooldown JumpCooldown { get;}



}
