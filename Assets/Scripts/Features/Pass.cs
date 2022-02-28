using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : MonoBehaviour
{
    public Ball ball;



    [SerializeField]
    private float cooldownTimePass = 500f;
    private CooldownManualReset cooldownForPass;

    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;
        cooldownForPass = new CooldownManualReset(cooldownTimePass);
    }


    public void Pass_(Vector3 targetPosition, float velocityMagnitude, float wait)
    {

        if (cooldownForPass.TimeOver() &&passFinished && ball.IsOwner(gameObject))
        {
            StartCoroutine(Pass__(targetPosition, velocityMagnitude, wait));
        }

    }


    private bool passFinished = true;
    private IEnumerator Pass__(Vector3 targetPosition, float velocityMagnitude, float wait)
    {

        passFinished = false;

        yield return new WaitForSeconds(wait);
        ball.RemoveOwner(gameObject);
        ball.Movement.MyMovePosition(targetPosition, velocityMagnitude);
        cooldownForPass.ResetTimer();

        passFinished = true;

    }





}
