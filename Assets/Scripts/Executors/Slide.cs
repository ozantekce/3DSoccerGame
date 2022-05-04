using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide
{

    public static void Slide_(Slideable slideable,float slideInput)
    {

        slideable.IsSliding = false;
        if (slideable.SlideCooldown.NotReady())
            return;

        GameObject gameObject = slideable.GameObject;

        Vector3 targetPosition = gameObject.transform.position + gameObject.transform.forward * 5f;

        Movement.MovePosition(slideable,targetPosition,0.1f,slideable.SlidePower);

    }



}
