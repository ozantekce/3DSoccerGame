using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Shoot : MonoBehaviour
{


    public Ball ball;

    [SerializeField]
    private float maxShootMagnitude;
    [SerializeField]
    private float cooldownTimeShoot = 500f;
    private CooldownManualReset cooldownForShoot;
    

    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;
        cooldownForShoot = new CooldownManualReset(cooldownTimeShoot);

    }


    public void Shoot_(Vector3 velocity, float wait)
    {

        if (cooldownForShoot.TimeOver() && shootFinished && ball.IsOwner(gameObject))
        {
            StartCoroutine(Shoot__(velocity,wait));
        }

    }


    private bool shootFinished = true;
    private IEnumerator Shoot__(Vector3 velocity, float wait)
    {   

        shootFinished = false;

        yield return new WaitForSeconds(wait);

        ball.RemoveOwner(gameObject);
        velocity = VectorCalculater.PreventToPassMaxMagnitude(velocity, maxShootMagnitude);
        ball.Movement.GiveVelocity(velocity);

        cooldownForShoot.ResetTimer();

        shootFinished = true;

    }





}
