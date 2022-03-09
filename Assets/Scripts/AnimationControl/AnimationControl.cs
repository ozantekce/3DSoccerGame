using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public abstract class AnimationControl : MonoBehaviour
{


    private Animator animator;

    private AnimationStatus currentStatus;
    private AnimationStatus nextStatus;

    private float startPlayTime;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPlayTime = Time.realtimeSinceStartup;
    }


    public class AnimationStatus
    {

        public string name;
        public int priority;
        public float minPlayTime;
        public float maxPlayTime;
        
    }

    private List<AnimationStatus> animationStatus = new List<AnimationStatus>();

    protected void AddAnimationStatus(AnimationStatus status)
    {
        animationStatus.Add(status);
    }


    public void ChangeAnimation(string name)
    {

        AnimationStatus nextAnimationStatus = null;
        for (int i = 0; i < animationStatus.Count; i++)
        {

            if(animationStatus[i].name == name)
            {
                nextAnimationStatus = animationStatus[i];
                break;
            }
        }

        if (nextAnimationStatus == null )
            return;

        if(currentStatus == null)
        {
            currentStatus = nextAnimationStatus;
            return;
        }

        if(nextStatus == null)
        {
            nextStatus = nextAnimationStatus;
            return;
        }
            

        bool c_higherPriority = nextAnimationStatus.priority > nextStatus.priority;

        if (c_higherPriority)
            nextStatus = nextAnimationStatus;



    }

    Rigidbody rb;
    private void Update()
    {

        animator.SetFloat("MovementSpeed", 0.5f +  (rb.velocity.magnitude/25f));
        if(currentStatus.maxPlayTime < ElapsedTime())
        {
            ChangeAnimation("Idle");
        }

    }

    private void LateUpdate()
    {
        
        if(nextStatus != null)
        {

            bool c_MinTimeOver = currentStatus.minPlayTime < ElapsedTime();
            if (c_MinTimeOver)
            {
                bool c_same = currentStatus == nextStatus;

                currentStatus = nextStatus;
                startPlayTime = Time.realtimeSinceStartup;

                //animator.Play(currentStatus.name);
                if(!c_same)
                    animator.CrossFade(currentStatus.name,0.03f);

            }
            nextStatus = null;
        }

        


    }



    private float ElapsedTime()
    {

        return (Time.realtimeSinceStartup - startPlayTime) * 1000;

    }


}
