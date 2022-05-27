using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Features/BallVision")]
public class BallVision : MonoBehaviour
{

    public float visionRadius = 3f;
    [Range(0f, 360f)]
    public float visionAngle = 60f;



    private LayerMask targetMask; // it must be only ball layer
    public LayerMask obstacleMask;

    private Cooldown cooldownWaitToTakeBall;
    public float cdToTakeBall = 1000f;

    public Transform ballTransform;

    public bool ControlBall()
    {
        return ballTransform != null && !Ball.Instance.Rigidbody.isKinematic;
    }

    public bool NoControlBall()
    {
        return !ControlBall();
    }

    Footballer footballer;

    void Start()
    {
        footballer = GetComponent<Footballer>();
        targetMask.value = 64;
        StartCoroutine("FindTargetsWithDelay", delay);
        cooldownWaitToTakeBall = new Cooldown(cdToTakeBall);
    }


    bool last;
    float delay;
    private void FixedUpdate()
    {

        if(ControlBall())
        {
            if(!Ball.Instance.controllers.Contains(footballer))
                Ball.Instance.AddController(footballer);
        }
        else
        {
            if (Ball.Instance.controllers.Contains(footballer))
                Ball.Instance.RemoveController(footballer);
        }

        last = ControlBall();

    }


    public Cooldown CooldownWaitToTakeBall { get => cooldownWaitToTakeBall; set => cooldownWaitToTakeBall = value; }

    IEnumerator FindTargetsWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            delay = 0.2f;
            //if(cooldownWaitToTakeBall.Ready())
                FindVisibleTargets();
        
        }

    }


    void FindVisibleTargets()
    {
        ballTransform = null;

        Collider[] targetsInVisionRadius = Physics.OverlapSphere(gameObject.transform.position, visionRadius, targetMask);

        for (int i = 0; i < targetsInVisionRadius.Length; i++)
        {

            Transform target = targetsInVisionRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < visionAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    float disY = Mathf.Abs(transform.position.y-target.position.y);
                    //Debug.Log("disY : " + disY);
                    if(disY < 1.5f)
                        ballTransform = target;

                }

            }

        }

    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }



}
