using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperActionMethods
{
    
    public static void GoRightPosition(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Animator animator = goalkeeper.Animator;

        Vector3 targetPos = goalkeeper.CalculateRightPosition();


        Movement.MovePosition(goalkeeper, targetPos, 0.5f,goalkeeper.MovementSpeed);
        Movement.SpinToObject(goalkeeper, Ball.Instance.gameObject, 8f);


        Rigidbody rigidbody = goalkeeper.Rigidbody;
        Vector3 velocityDirection = rigidbody.velocity.normalized;
        Vector3 goalkeeperForward = goalkeeper.transform.forward;

        // hizin yönü ile kalecinin baktiði yon arasindaki aci
        float angleBetween = Vector3.Angle(goalkeeperForward, velocityDirection);

        //  going backward
        if (angleBetween > 120f)
        {
            animator.SetInteger("Walking", 2);
        }
        else
        {
            //going right
            if (VectorCalculater.PositionIs_(goalkeeper.transform, targetPos, Direction.right))
            {
                animator.SetInteger("Walking", 3);
            }
            //going left
            else
            {
                animator.SetInteger("Walking", 4);
            }
        }



    }

    public static void GrabBall(FiniteStateMachine fsm)
    {

        if (Ball.Instance.Rigidbody.isKinematic)
            return;

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Animator animator = goalkeeper.Animator;
        animator.SetTrigger("Scoop");

        goalkeeper.Rigidbody.isKinematic = true;

        Ball ball = Ball.Instance;
        ball.Rigidbody.isKinematic = true;

        fsm.StartCoroutine(GrabBall_(fsm,3f));


    }


    public static void CatchBall(FiniteStateMachine fsm)
    {
        if (Ball.Instance.Rigidbody.isKinematic)
            return;

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Animator animator = goalkeeper.Animator;
        //Debug.Log("catch");

        Ball ball = Ball.Instance;
        ball.Rigidbody.isKinematic = true;

        fsm.StartCoroutine(CatchBall_(fsm,1.8f));

    }


    public static void GoToBallPosition(FiniteStateMachine fsm)
    {

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Animator animator = goalkeeper.Animator;
        animator.SetInteger("Walking", 1);
        

        Movement.SpinToObject(goalkeeper, Ball.Instance.gameObject, 10f);
        Movement.MovePosition(goalkeeper, Ball.Instance.transform.position, 0.5f, goalkeeper.MovementSpeed);




    }


    public static void LookTheBall(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Movement.SpinToObject(goalkeeper, Ball.Instance.gameObject, 8f);

    }


    private static IEnumerator GrabBall_(FiniteStateMachine fsm, float duration)
    {
        //Debug.Log("start");
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        float T = 0;
        
        while (true)
        {

            T += Time.deltaTime;
            float t01 = T / duration;

            Vector3 startPosition = Ball.Instance.transform.position;
            Vector3 endPosition = goalkeeper.RightHand.transform.position;
            Vector3 pos = Vector3.Lerp(startPosition, endPosition, t01);

            Ball.Instance.transform.position = pos;

            if (t01 >= 1)
            {
                Ball.Instance.Collider.isTrigger = true;
                //fsm.GetComponent<Goalkeeper>().RightHand
                break;
            }

            yield return new WaitForEndOfFrame();

        }


        /*
        while (goalkeeper.CurrentState == GoalkeeperJumpState.goalkeeperJumpState)
        {

            yield return new WaitForEndOfFrame();

        }*/
        //Debug.Log("over");

    }

    private static IEnumerator CatchBall_(FiniteStateMachine fsm, float duration)
    {

        //Debug.Log("start");
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        

        float T = 0;

        while (true)
        {

            T += Time.deltaTime;
            float t01 = T / duration;

            Vector3 startPosition = Ball.Instance.transform.position;
            Vector3 endPosition = goalkeeper.RightHand.transform.position;
            Vector3 pos = Vector3.Lerp(startPosition, endPosition, t01);

            Ball.Instance.transform.position = pos;

            if (t01 >= 1)
            {
                Ball.Instance.Collider.isTrigger = true;
                //fsm.GetComponent<Goalkeeper>().RightHand
                break;
            }

            yield return new WaitForEndOfFrame();

        }

        //Debug.Log("over");

    }



    public static void ThrowBall(FiniteStateMachine fsm)
    {


        Ball.Instance.Collider.isTrigger = false;

        if (ThrowBallRunning==false)
            fsm.StartCoroutine(ThrowBall_(fsm));

    }


    public static void LookRivalGoalpost(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Movement.SpinToObject(goalkeeper, goalkeeper.GoalpostCenterRival.gameObject, 15f);
    
    }

    private static bool ThrowBallRunning = false;
    private static IEnumerator ThrowBall_(FiniteStateMachine fsm)
    {
        //Debug.Log("start");
        
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        ThrowBallRunning = true;
        Vector3 velocity = fsm.transform.forward;
        velocity.y = 0.2f;
        //Debug.Log("vel : "+velocity);
        velocity = velocity.normalized * 30f;
        int times = 0;
        
        while (times<=20)
        {
            Ball.Instance.transform.Translate(velocity*Time.deltaTime,Space.World);
            yield return new WaitForEndOfFrame();
            times++;
        }
        //Debug.Log("over");
        Ball.Instance.Rigidbody.isKinematic = false;
        yield return new WaitForEndOfFrame();
        Shot.ForceShot(goalkeeper, 1, 0, 0);
        ThrowBallRunning = false;
        goalkeeper.Rigidbody.isKinematic = false;

    }



    public static void HoldTheBall(FiniteStateMachine fsm)
    {
        if (!Ball.Instance.Collider.isTrigger)
            return;
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Ball.Instance.transform.position = goalkeeper.RightHand.transform.position;

    }


    public static void Stop(FiniteStateMachine fsm)
    {

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        goalkeeper.Rigidbody.velocity
            = VectorCalculater.VectorZeroWithoutY(goalkeeper.Rigidbody.velocity);

        Animator animator = goalkeeper.Animator;
        animator.SetInteger("Walking", 0);


    }


    public static void PlayJumpAnim(FiniteStateMachine fsm)
    {


        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Jump");

    }


    public static void PlayThrowAnim(FiniteStateMachine fsm)
    {


        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Throw");

    }


    public static void Jump(FiniteStateMachine fsm)
    {


        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Rigidbody rigidbody = goalkeeper.Rigidbody;

        Vector3[] info = GoalkeeperCalculater.CalculateAll_(goalkeeper);



        Vector3 meetingPosition = info[0];
        Vector3 requiredVelocity = info[1];

        Animator animator = fsm.GetComponent<Animator>();

        

        if (goalkeeper.CenterUp.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("CenterUp and mt : " + meetingPosition);

            animator.SetInteger("JumpVal", 0);

        }
        else if (goalkeeper.Center.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("Center and mt : " + meetingPosition);
            animator.SetInteger("JumpVal", 1);
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
            animator.SetInteger("JumpVal", 4);
        }
        else if (goalkeeper.LeftUp.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("LeftUp and mt : " + meetingPosition);
            animator.SetInteger("JumpVal", 5);

        }
        else if (goalkeeper.LeftDown.IntersectWithMeetingPosition(meetingPosition))
        {
            //Debug.Log("LeftDown and mt : " + meetingPosition);
            animator.SetInteger("JumpVal", 6);
        }
        else
        {

            if (VectorCalculater.PositionIs_(fsm.transform, meetingPosition, Direction.right))
            {
                //Debug.Log("other right and mt : " + meetingPosition);
                animator.SetInteger("JumpVal", 3);
            }
            else if (VectorCalculater.PositionIs_(fsm.transform, meetingPosition, Direction.left))
            {
                //Debug.Log("other left and mt : " + meetingPosition);
                animator.SetInteger("JumpVal", 5);
            }


        }


        animator.SetTrigger("Jump");

        float Vx = requiredVelocity.x;
        float Vy = requiredVelocity.y;
        rigidbody.velocity = requiredVelocity;
        
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
        rigidbody.velocity = Deformation.Deform(requiredVelocity,10f,40f);
        




    }




}
