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

    private CooldownManualReset cooldownWaitToTakeBall;
    public float cdToTakeBall = 1000f;

    public Transform ballTransform;

    public bool ControlBall()
    {
        return ballTransform != null && !Ball.Instance.Rb.isKinematic;
    }

    public bool NoControlBall()
    {
        return !ControlBall();
    }

    void Start()
    {
        targetMask.value = 64;
        StartCoroutine("FindTargetsWithDelay", .1f);
        cooldownWaitToTakeBall = new CooldownManualReset(cdToTakeBall);
    }


    

    public CooldownManualReset CooldownWaitToTakeBall { get => cooldownWaitToTakeBall; set => cooldownWaitToTakeBall = value; }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            if(cooldownWaitToTakeBall.TimeOver())
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