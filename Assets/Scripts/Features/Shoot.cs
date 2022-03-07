using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BallVision))]
public class Shoot : MonoBehaviour
{


    public Ball ball;

    private BallVision ballVision;

    [SerializeField]
    private float maxShootMagnitude;
    [SerializeField]
    private float cooldownTimeShoot = 500f;
    private CooldownManualReset cooldownForShoot;


    private static float defaultWait = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;
        ballVision = GetComponent<BallVision>();
        cooldownForShoot = new CooldownManualReset(cooldownTimeShoot);

    }


    public void Shoot_(Vector3 velocity, float wait)
    {
        
        if (cooldownForShoot.TimeOver() && shootFinished && ball.IsOwner(gameObject))
        {
            StartCoroutine(Shoot__(velocity,wait));
        }

    }

    public void Shoot_(Vector3 velocity)
    {

        if (cooldownForShoot.TimeOver() && shootFinished && ball.IsOwner(gameObject))
        {
            StartCoroutine(Shoot__(velocity, defaultWait));
        }

    }


    private bool shootFinished = true;
    private IEnumerator Shoot__(Vector3 velocity, float wait)
    {   

        shootFinished = false;
        ballVision.CooldownWaitToTakeBall.ResetTimer();
        yield return new WaitForSeconds(wait);

        ball.RemoveOwner(gameObject);
        velocity = VectorCalculater.PreventToPassMaxMagnitude(velocity, maxShootMagnitude);
        ball.Movement.GiveVelocity(velocity);

        cooldownForShoot.ResetTimer();

        shootFinished = true;

    }





}
