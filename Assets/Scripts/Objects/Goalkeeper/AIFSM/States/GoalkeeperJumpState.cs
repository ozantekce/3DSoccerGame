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

        AddAction(new MyAction(GoalkeeperActionMethods.CatchBall, ConditionMethods.GoalkeeperBallCatchable));

        AddTransition(new Transition(GoalkeeperIdleState.Instance, ConditionMethods.Elapsed4SecondInState));

        AddTransition(new Transition(GoalkeeperIdleWithBallState.Instance, ConditionMethods.BallCatched)
            ,RunTimeOfTransition.runOnPreExecution);



    }

    public override void EnterOptional(FiniteStateMachine fsm)
    {

        Debug.Log("Enter GoalkeeperJumpState");

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Rigidbody rigidbody = goalkeeper.Rigidbody;

        Vector3[] info = GoalkeeperCalculater.CalculateAll(goalkeeper);



        Vector3 meetingPosition = info[0];
        Vector3 requiredVelocity = info[1];

        Animator animator = fsm.GetComponent<Animator>();

        

        if (goalkeeper.CenterUp.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("CenterUp and mt : " + meetingPosition);
            animator.SetInteger("JumpVal",0);

        }
        else if (goalkeeper.Center.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("Center and mt : " + meetingPosition);
            animator.SetInteger("JumpVal",1);
        }
        else if (goalkeeper.CenterDown.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("CenterDown and mt : " + meetingPosition);
            animator.SetInteger("JumpVal", 2);
        }
        else if (goalkeeper.RightUp.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("RightUp and mt : " + meetingPosition);
            animator.SetInteger("JumpVal", 3);
        }
        else if (goalkeeper.RightDown.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("RightDown and mt : " + meetingPosition);
            animator.SetInteger("JumpVal",4);
        }
        else if (goalkeeper.LeftUp.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("LeftUp and mt : " + meetingPosition);
            animator.SetInteger("JumpVal", 5);

        }
        else if (goalkeeper.LeftDown.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("LeftDown and mt : " + meetingPosition);
            animator.SetInteger("JumpVal",6);
        }
        else
        {
            
            if (VectorCalculater.PositionIs_(fsm.transform, meetingPosition, Direction.right))
            {
                //Debug.Log("other right and mt : " + meetingPosition);
                animator.SetInteger("JumpVal", 3);
            }
            else if(VectorCalculater.PositionIs_(fsm.transform, meetingPosition, Direction.left)){
                //Debug.Log("other left and mt : " + meetingPosition);
                animator.SetInteger("JumpVal", 5);
            }


        }


        animator.SetTrigger("Jump");

        float Vx = requiredVelocity.x;
        float Vy = requiredVelocity.y;
        if (goalkeeper.JumpPowerY < Vy)
        {
            Vy = goalkeeper.JumpPowerY;
        }
        if (goalkeeper.JumpPowerX < Mathf.Abs(Vx))
        {
            Vx = goalkeeper.JumpPowerX * (Vx < 0 ? -1 : +1);
        }
        requiredVelocity.x = Vx;
        requiredVelocity.y = Vy;

        rigidbody.velocity = requiredVelocity;





    }

    public override void ExitOptional(FiniteStateMachine fsm)
    {

        
    }




}
