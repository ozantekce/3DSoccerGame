using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BallVision))]
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



    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;
        ballVision = GetComponent<BallVision>();
        inputter = GetComponent<Inputter>();
        cooldownForShoot = new CooldownManualReset(cooldownTimeShoot);

    }

    private void Update()
    {

        if (inputter.GetButtonShootValue() > 0)
            Shoot_(0.3f);

    }


    private void Shoot_(float wait)
    {
        
        if (cooldownForShoot.TimeOver() && shootFinished && ball.IsOwner(gameObject))
        {
            StartCoroutine(Shoot__(inputter.GetButtonShootValue() * shootPower * (transform.forward + new Vector3(0, 0.4f, 0)), wait));
        }

    }



    private bool shootFinished = true;
    private IEnumerator Shoot__(Vector3 velocity, float wait)
    {   

        shootFinished = false;
        ballVision.CooldownWaitToTakeBall.ResetTimer();
        yield return new WaitForSeconds(wait);

        ball.RemoveOwner(gameObject);

        ball.Rb.velocity += velocity;

        cooldownForShoot.ResetTimer();

        shootFinished = true;

    }





}
