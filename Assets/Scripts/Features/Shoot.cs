using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{


    public Ball ball;

    

    // Start is called before the first frame update
    void Start()
    {
        ball = Ball.Instance;
    }


    public void Shoot_(Vector3 velocity, float wait)
    {

        if (shootFinished && ball.Owner == this.gameObject)
        {
            StartCoroutine(Shoot__(velocity,wait));
        }

    }


    private bool shootFinished = true;
    private IEnumerator Shoot__(Vector3 velocity, float wait)
    {   

        shootFinished = false;

        yield return new WaitForSeconds(wait);
        ball.Shoot(velocity);
        
        shootFinished = true;

    }





}
