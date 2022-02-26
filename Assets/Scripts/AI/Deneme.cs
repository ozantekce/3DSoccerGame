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
            Debug.Log(requireVelocity);
            jump.Jump_(requireVelocity);
        }
        else
        {
            /*
            if(requireVelocity != Vector3.zero && Mathf.Abs(requireVelocity.z)<=1f)
                Debug.Log(requireVelocity);
            */
        }


    }


    private bool NeedJump(Vector3 requireVelocity)
    {

        bool c_noRequireVelocity = requireVelocity != Vector3.zero;
        bool c_velocity = ball.GetVelocity().magnitude >= minBallVelocityToJump;
        bool c_reqVelocity = Mathf.Abs(requireVelocity.x) <= maxJumpVelocityX && Mathf.Abs(requireVelocity.y) <= maxJumpVelocityY;
        bool c_dontJumpZAxis = Mathf.Abs(requireVelocity.z) <= 1f;
        

        return c_noRequireVelocity && c_velocity && c_reqVelocity && c_dontJumpZAxis;

    }







}
