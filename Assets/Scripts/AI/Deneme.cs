using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement),typeof(Jump),typeof(BallVision))]
public class Deneme : MonoBehaviour
{

    private Ball ball;

    private Jump jump;

    public float minBallVelocityToJump;
    public float minDistanceToJump;

    public float maxJumpVelocityX;
    public float maxJumpVelocityY;
    public float maxJumpVelocityZ;

    // Start is called before the first frame update
    void Start()
    {
        jump = GetComponent<Jump>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == null)
            ball = Ball.Instance;
        
        Jump();
    }


    private void Jump()
    {


        Vector3 requireVelocity 
            = GoalkeeperCalculater.FindRequiredVelocity(transform.position, ball.transform.position, ball.GetVelocity());

        if (NeedJump(requireVelocity))
        {
            //Debug.Log(requireVelocity);
            jump.Jump_(requireVelocity,0.01f);
        }

    }


    private bool NeedJump(Vector3 requireVelocity)
    {

        bool c_ballFrontOfThis = VectorCalculater.CheckVector2FrontOfVector1(transform.position, ball.transform.position);

        
        bool c_requireVelocityLowThenMax = Mathf.Abs(requireVelocity.x) < maxJumpVelocityX && Mathf.Abs(requireVelocity.y) < maxJumpVelocityY && Mathf.Abs(requireVelocity.z)< maxJumpVelocityZ;

        

        

        return c_ballFrontOfThis && c_requireVelocityLowThenMax ;

    }







}
