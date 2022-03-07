using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BallVision))]
public class Pass : MonoBehaviour
{

    [SerializeField]
    private float passPower;

    public Ball ball;

    private BallVision ballVision;

    private Inputter inputter;

    [SerializeField]
    private float cooldownTimePass = 500f;
    private CooldownManualReset cooldownForPass;


    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;
        ballVision = GetComponent<BallVision>();
        cooldownForPass = new CooldownManualReset(cooldownTimePass);
        inputter = GetComponent<Inputter>();
    }

    private void Update()
    {
        if (inputter.GetButtonPassValue() > 0)
            Pass_(new Vector3(0,0,0),0.3f);
    }

    private void Pass_(Vector3 targetPosition, float wait)
    {

        if (cooldownForPass.TimeOver() &&passFinished && ball.IsOwner(gameObject))
        {
            StartCoroutine(Pass__(targetPosition, wait));
        }


    }


    private bool passFinished = true;
    private IEnumerator Pass__(Vector3 targetPosition, float wait)
    {

        passFinished = false;
        ballVision.CooldownWaitToTakeBall.ResetTimer();
        yield return new WaitForSeconds(wait);
        ball.RemoveOwner(gameObject);
        ball.MyMovePosition(targetPosition, passPower);
        cooldownForPass.ResetTimer();

        passFinished = true;

    }





}
