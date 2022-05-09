using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperActionMethods
{
    
    public static void TakePosition(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Animator animator = goalkeeper.Animator;

        Vector3 targetPos = goalkeeper.CalculateRightPosition();
        /*
        Vector3 localDir
            = Quaternion.Inverse(goalkeeper.transform.rotation) * (targetPos - goalkeeper.transform.position);

        bool isForward = localDir.z > 0;
        bool isUp = localDir.y > 0;
        bool isRight = localDir.x > 0;
        */






        Movement.MovePosition(goalkeeper, targetPos, 0.5f,goalkeeper.MovementSpeed);
        Movement.SpinToObject(goalkeeper, Ball.Instance.gameObject, 15f);


        Rigidbody rigidbody = goalkeeper.Rigidbody;
        Vector3 velocityDirection = rigidbody.velocity.normalized;
        Vector3 goalkeeperForward = goalkeeper.transform.forward;

        float angleBetween = Vector3.Angle(goalkeeperForward, velocityDirection);

        if (angleBetween > 120f)
        {
            animator.SetInteger("Walking", 2);
        }
        else
        {
            if (VectorCalculater.PositionIs_(goalkeeper.transform, targetPos, Direction.right))
            {
                animator.SetInteger("Walking", 3);
            }
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
        animator.SetBool("Catched",true);

        Debug.Log("catch");

        Ball ball = Ball.Instance;
        ball.Rigidbody.isKinematic = true;

        fsm.StartCoroutine(SendBallToHands2(fsm,0.8f));

    }


    public static void GoToBall(FiniteStateMachine fsm)
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
        Debug.Log("start");
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
        Debug.Log("over");

    }

    private static IEnumerator SendBallToHands2(FiniteStateMachine fsm, float duration)
    {

        Debug.Log("start");
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

        Debug.Log("over");

    }



    public static void GoalkeeperThrowBall(FiniteStateMachine fsm)
    {


        Ball.Instance.Collider.isTrigger = false;

        if (ThrowBallRunning==false)
            fsm.StartCoroutine(ThrowBall(fsm));

    }


    public static void GoalkeeperLookRivalGoalPost(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Movement.SpinToObject(goalkeeper, goalkeeper.GoalpostCenterRival.gameObject, 15f);
    
    }

    private static bool ThrowBallRunning = false;
    private static IEnumerator ThrowBall(FiniteStateMachine fsm)
    {
        //Debug.Log("start");
        
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        ThrowBallRunning = true;
        Vector3 velocity = fsm.transform.forward;
        velocity.y = 0.2f;
        //Debug.Log("vel : "+velocity);
        velocity = velocity.normalized * 30f;
        int times = 0;
        
        while (times<=30)
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



    public static void GoalkeeperHoldTheBall(FiniteStateMachine fsm)
    {
        if (!Ball.Instance.Collider.isTrigger)
            return;
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        Ball.Instance.transform.position = goalkeeper.RightHand.transform.position;

    }






}
