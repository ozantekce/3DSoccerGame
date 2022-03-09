using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BallVision))]
[AddComponentMenu("Features/Pass")]
public class Pass : MonoBehaviour
{

    [SerializeField]
    private float passPower;

    public Ball ball;

    private BallVision ballVision;

    private Inputter inputter;
    private AnimationControl animationControl;


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
        animationControl = GetComponent<AnimationControl>();
    }

    private void Update()
    {
        if (inputter.GetButtonPassValue() > 0)
            Pass_(new Vector3(0,0,0),0.3f);
    }

    private void Pass_(Vector3 targetPosition, float wait)
    {

        if (cooldownForPass.TimeOver() &&passFinished && ballVision.IsThereBallInVision())
        {
            StartCoroutine(Pass__(targetPosition, wait));
        }


    }


    private bool passFinished = true;
    private IEnumerator Pass__(Vector3 targetPosition, float wait)
    {

        passFinished = false;
        animationControl.ChangeAnimation("Shoot");
        yield return new WaitForSeconds(wait);
        ballVision.CooldownWaitToTakeBall.ResetTimer();
        ballVision.ballTransform = null;
        ball.MyMovePosition(targetPosition, passPower);
        cooldownForPass.ResetTimer();

        passFinished = true;

    }





}
