using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperJumpState : State
{

    private readonly static GoalkeeperJumpState instance = new GoalkeeperJumpState();
    public static GoalkeeperJumpState Instance => instance;
    private GoalkeeperJumpState()
    {



    }


    public override void Init()
    {

        AddAction(new MyAction(ActionMethods.GoalkeeperCatchBall, ConditionMethods.GoalkeeperBallCatchable));

        AddTransition(new Transition(GoalkeeperIdleState.Instance, ConditionMethods.Elapsed4SecondInState));

        AddTransition(new Transition(GoalkeeperIdleWithBallState.Instance, ConditionMethods.BallCatched)
            ,RunTimeOfTransition.runOnPreExecution);



    }

    public override void Enter_(FiniteStateMachine fsm)
    {

        Debug.Log("Enter GoalkeeperJumpState");



        Goalkeeper goalkeeper = fsm.GetComponent<Goalkeeper>();


        Vector3 meetingPosition
            = GoalkeeperCalculater.FindMeetingPosition(goalkeeper, Ball.Instance.transform.position, Ball.Instance.Rb.velocity);

        Animator animator = fsm.GetComponent<Animator>();

        

        if (goalkeeper.CenterUp.IntersectWithMeetingPosition(meetingPosition))
        {
            Debug.Log("CenterUp and mt : " + meetingPosition);
            animator.SetInteger("JumpVal",0);

        }
        else if (goalkeeper.Center.IntersectWithMeetingPosition(meetingPosition))
        {
            Debug.Log("Center and mt : " + meetingPosition);
            animator.SetInteger("JumpVal",1);
        }
        else if (goalkeeper.CenterDown.IntersectWithMeetingPosition(meetingPosition))
        {
            Debug.Log("CenterDown and mt : " + meetingPosition);
            animator.SetInteger("JumpVal", 2);
        }
        else if (goalkeeper.RightUp.IntersectWithMeetingPosition(meetingPosition))
        {
            Debug.Log("RightUp and mt : " + meetingPosition);
            animator.SetInteger("JumpVal", 3);
        }
        else if (goalkeeper.RightDown.IntersectWithMeetingPosition(meetingPosition))
        {
            Debug.Log("RightDown and mt : " + meetingPosition);
            animator.SetInteger("JumpVal",4);
        }
        else if (goalkeeper.LeftUp.IntersectWithMeetingPosition(meetingPosition))
        {
            Debug.Log("LeftUp and mt : " + meetingPosition);
            animator.SetInteger("JumpVal", 5);

        }
        else if (goalkeeper.LeftDown.IntersectWithMeetingPosition(meetingPosition))
        {
            Debug.Log("LeftDown and mt : " + meetingPosition);
            animator.SetInteger("JumpVal",6);
        }
        else
        {
            
            if (VectorCalculater.PositionIs_(fsm.transform, meetingPosition, Direction.right))
            {
                Debug.Log("other right and mt : " + meetingPosition);
                animator.SetInteger("JumpVal", 3);
            }
            else if(VectorCalculater.PositionIs_(fsm.transform, meetingPosition, Direction.left)){
                Debug.Log("other left and mt : " + meetingPosition);
                animator.SetInteger("JumpVal", 5);
            }


        }


        animator.SetTrigger("Jump");

        goalkeeper.Rigidbody.velocity = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper, Ball.Instance.transform.position, Ball.Instance.Rb.velocity);

        Debug.Log("old goalkeeper pos : " + fsm.transform.position + " meeting pos : " + meetingPosition
    + " velocity : " + goalkeeper.Rigidbody.velocity);
        
        Vector3 velocity = goalkeeper.Rigidbody.velocity;


        if (velocity.x < 0)
        {
            velocity.x = Mathf.Clamp(velocity.x, -goalkeeper.JumpPowerX, 0);
        }
        else
        {
            velocity.x = Mathf.Clamp(velocity.x, 0, goalkeeper.JumpPowerX);
        }

        velocity.y = Mathf.Clamp(velocity.y, 1f, goalkeeper.JumpPowerY);


        goalkeeper.Rigidbody.velocity = Deformation.Deform(velocity,10f,30f);

        Debug.Log("new goalkeeper pos : " + fsm.transform.position + " meeting pos : " + meetingPosition
+ " velocity : " + goalkeeper.Rigidbody.velocity);
        

    }

    public override void Exit_(FiniteStateMachine fsm)
    {

        
    }




}
