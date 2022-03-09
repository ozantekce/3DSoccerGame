using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BallVision))]
[AddComponentMenu("Features/Shoot")]
public class Shoot : MonoBehaviour
{





    public Ball ball;

    private BallVision ballVision;

    private Inputter inputter;

    [SerializeField]
    private float shootPower;
    [SerializeField]
    private float cooldownTimeShoot = 500f;
    private CooldownManualReset cooldownForShoot;

    private AnimationControl animationControl;


    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;
        ballVision = GetComponent<BallVision>();
        inputter = GetComponent<Inputter>();
        cooldownForShoot = new CooldownManualReset(cooldownTimeShoot);
        animationControl = GetComponent<AnimationControl>();
    }

    private void Update()
    {

        if (inputter.GetButtonShootValue() > 0)
            Shoot_(0.3f);

    }


    private void Shoot_(float wait)
    {
        
        if (cooldownForShoot.TimeOver() && shootFinished && ballVision.IsThereBallInVision())
        {
            StartCoroutine(Shoot__(inputter.GetButtonShootValue() * shootPower * (transform.forward + new Vector3(0, 0.4f, 0)), wait));
        }

    }



    private bool shootFinished = true;
    private IEnumerator Shoot__(Vector3 velocity, float wait)
    {   

        shootFinished = false;
        animationControl.ChangeAnimation("Shoot");

        yield return new WaitForSeconds(wait);
        ballVision.CooldownWaitToTakeBall.ResetTimer();
        ballVision.ballTransform = null;
        ball.Rb.velocity += velocity;

        cooldownForShoot.ResetTimer();

        shootFinished = true;

    }





}
