using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BallVision))]
public class Pass : MonoBehaviour
{
    public Ball ball;

    private BallVision ballVision;

    [SerializeField]
    private float cooldownTimePass = 500f;
    private CooldownManualReset cooldownForPass;

    private static float defaultWait = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;
        ballVision = GetComponent<BallVision>();
        cooldownForPass = new CooldownManualReset(cooldownTimePass);
    }


    public void Pass_(Vector3 targetPosition, float velocityMagnitude, float wait)
    {

        if (cooldownForPass.TimeOver() &&passFinished && ball.IsOwner(gameObject))
        {
            StartCoroutine(Pass__(targetPosition, velocityMagnitude, wait));
        }


    }

    public void Pass_(Vector3 targetPosition, float velocityMagnitude)
    {

        if (cooldownForPass.TimeOver() && passFinished && ball.IsOwner(gameObject))
        {
            StartCoroutine(Pass__(targetPosition, velocityMagnitude, defaultWait));
        }


    }


    private bool passFinished = true;
    private IEnumerator Pass__(Vector3 targetPosition, float velocityMagnitude, float wait)
    {

        passFinished = false;
        ballVision.CooldownWaitToTakeBall.ResetTimer();
        yield return new WaitForSeconds(wait);
        ball.RemoveOwner(gameObject);
        ball.Movement.MyMovePosition(targetPosition, velocityMagnitude);
        cooldownForPass.ResetTimer();

        passFinished = true;

    }





}
