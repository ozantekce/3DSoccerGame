using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : AnimationControl
{

    

    private void Start()
    {
        base.Start();

        AnimationStatus idle = new AnimationStatus();
        idle.name = "Idle";
        idle.priority = 0;
        idle.minPlayTime = -1;
        idle.maxPlayTime = 999999;

        AnimationStatus run = new AnimationStatus();
        run.name = "Run";
        run.priority = 1;
        run.minPlayTime = 10;
        run.maxPlayTime = 100;

        AnimationStatus shoot = new AnimationStatus();
        shoot.name = "Shoot";
        shoot.priority = 5;
        shoot.minPlayTime = 1000;
        shoot.maxPlayTime = 1000;


        AnimationStatus pass = new AnimationStatus();
        pass.name = "Pass";
        pass.priority = 4;
        pass.minPlayTime = 1000;
        pass.maxPlayTime = 1000;

        AnimationStatus run2 = new AnimationStatus();
        run2.name = "Run2";
        run2.priority = 1;
        run2.minPlayTime = 1000;
        run2.maxPlayTime = 1000;


        AddAnimationStatus(idle);
        AddAnimationStatus(run);
        AddAnimationStatus(shoot);
        AddAnimationStatus(pass);
        AddAnimationStatus(run2);


        ChangeAnimation("Idle");

    }



}
