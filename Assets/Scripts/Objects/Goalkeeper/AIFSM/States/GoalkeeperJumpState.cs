using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperJumpState : State
{

    private readonly static GoalkeeperJumpState instance = new GoalkeeperJumpState();
    public static GoalkeeperJumpState Instance => instance;
    private GoalkeeperJumpState()
    {
        Debug.Log("GoalkeeperJumpState created");


    }


    public override void Init(FiniteStateMachine fsm)
    {

        AddAction(ActionMethods.GoalkeeperCatchBall, ConditionMethods.GoalkeeperBallCatchable);

        AddTransition(GoalkeeperIdleState.Instance, ConditionMethods.Elapsed2SecondInState);

        AddTransition(GoalkeeperIdleWithBallState.Instance, ConditionMethods.BallCatched);



    }

    public override void Enter(FiniteStateMachine fsm)
    {

        Debug.Log("Enter GoalkeeperJumpState");



        Goalkeeper goalkeeper = fsm.GetComponent<Goalkeeper>();
        Vector3 meetingPosition
            = GoalkeeperCalculater.FindMeetingPosition(goalkeeper.transform.position, Ball.Instance.transform.position, Ball.Instance.Rb.velocity);

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
                Debug.Log("RightDown and mt : " + meetingPosition);
                animator.SetInteger("JumpVal", 4);
            }
            else if(VectorCalculater.PositionIs_(fsm.transform, meetingPosition, Direction.left)){
                Debug.Log("LeftDown and mt : " + meetingPosition);
                animator.SetInteger("JumpVal", 6);
            }


        }


        animator.SetTrigger("Jump");

        goalkeeper.Rigidbody.velocity = GoalkeeperCalculater.FindRequiredVelocity(goalkeeper.transform.position, Ball.Instance.transform.position, Ball.Instance.Rb.velocity);

    }

    public override void Exit(FiniteStateMachine fsm)
    {
        Debug.Log("Exit GoalkeeperJumpState");


    }




}
