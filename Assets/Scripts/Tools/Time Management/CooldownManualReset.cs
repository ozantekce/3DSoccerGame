using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// no auto reset
public class CooldownManualReset
{
    private float cooldown;
    private float lastTime;

    private bool first = true;

    public CooldownManualReset(float cooldown)
    {
        this.cooldown = cooldown;
        lastTime = Time.realtimeSinceStartup;
    }

    public void SetCooldown(float cooldown)
    {
        this.cooldown = cooldown;
    }

    public bool TimeOver()
    {

        if (ElapsedTime() >= cooldown)
        {
            first = false;
            return true;
        }
        else
            return false||first;

    }

    public void ResetTimer()
    {
        first = false;
        lastTime = Time.realtimeSinceStartup;
    }

    public float ElapsedTime()
    {

        return (Time.realtimeSinceStartup - lastTime) * 1000;
    }
}
