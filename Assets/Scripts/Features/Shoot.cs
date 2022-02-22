using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{


    public Ball ball;

    private Movement ballMovement;

    

    // Start is called before the first frame update
    void Start()
    {
        ballMovement = ball.gameObject.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot_(Vector3 velocity, float wait)
    {

        if (shootFinished && ball.owner == this.gameObject)
        {
            StartCoroutine(Shoot__(velocity,wait));
        }

    }


    private bool shootFinished = true;
    private IEnumerator Shoot__(Vector3 velocity, float wait)
    {

        
        shootFinished = false;


        yield return new WaitForSeconds(wait);
        ballMovement.GiveVelocity(velocity);
        
        shootFinished = true;

    }





}
