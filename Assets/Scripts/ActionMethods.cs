using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMethods
{
    


    public static void FootballerRunMethod(FiniteStateMachine fsm)
    {

        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        
        float verticalInput = footballer.verticalInput;
        float horizontalInput = footballer.horizontalInput;
        Movement.SpinAndMoveForward(footballer, verticalInput, horizontalInput);

    }

    public static void FootballerDribblingMethod(FiniteStateMachine fsm)
    {
        Footballer footballer = ((FootballerFSM)fsm).Footballer;

        float verticalInput = footballer.verticalInput;
        float horizontalInput = footballer.horizontalInput;


        Dribbling.Dribbling_(footballer, verticalInput, horizontalInput);

    }


    public static void ShotMethod(FiniteStateMachine fsm)
    {

        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        float verticalInput = footballer.verticalInput;
        float horizontalInput = footballer.horizontalInput;
        float shotInput = footballer.shotInput;
        Shot.Shot_(footballer, shotInput, verticalInput, horizontalInput);
        

    }

    public static void PassMethod(FiniteStateMachine fsm)
    {

        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        float passInput = footballer.passInput;
        Pass.Pass_(footballer, passInput, footballer.TeamMate.transform);
        
    }

    public static void SlideMethod(FiniteStateMachine fsm)
    {

        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        Slide.Slide_(footballer, 1);
        

    }

    public static void FallMethod(FiniteStateMachine fsm)
    {

        Footballer footballer = ((FootballerFSM)fsm).Footballer;
        Fall.Fall_(footballer);


    }



    public static void GoalkeeperTakePosition(FiniteStateMachine fsm)
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
        if (VectorCalculater.PositionIs_(goalkeeper.transform,targetPos,Direction.right))
        {
            animator.SetBool("WalkingLeft", true);
        }
        else
        {
            animator.SetBool("WalkingRight", true);
        }

        Movement.MovePosition(goalkeeper, targetPos, 1.5f,goalkeeper.MovementSpeed);
        Movement.SpinToObject(goalkeeper, Ball.Instance.gameObject, 15f);



    }

    public static void GoalkeeperGrabBall(FiniteStateMachine fsm)
    {

        if (Ball.Instance.Rb.isKinematic)
            return;

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Animator animator = goalkeeper.Animator;
        animator.SetTrigger("Scoop");

        goalkeeper.Rigidbody.isKinematic = true;

        Ball ball = Ball.Instance;
        ball.Rb.isKinematic = true;

        fsm.StartCoroutine(SendBallToHands(fsm,0.8f));


    }


    public static void GoalkeeperCatchBall(FiniteStateMachine fsm)
    {
        if (Ball.Instance.Rb.isKinematic)
            return;

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Animator animator = goalkeeper.Animator;
        animator.SetBool("Catched",true);

        Debug.Log("catch");

        Ball ball = Ball.Instance;
        ball.Rb.isKinematic = true;

        fsm.StartCoroutine(SendBallToHands2(fsm,0.1f));

    }


    public static void GoalkeeperGoToBall(FiniteStateMachine fsm)
    {

        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;

        Movement.SpinToObject(goalkeeper, Ball.Instance.gameObject, 10f);
        Movement.MovePosition(goalkeeper, Ball.Instance.transform.position, 0.5f, goalkeeper.MovementSpeed);

    }



    private static IEnumerator SendBallToHands(FiniteStateMachine fsm, float duration)
    {
        Debug.Log("start");
        Goalkeeper goalkeeper = ((GoalkeeperFSM)fsm).Goalkeeper;
        float T = 0;
        Ball.Instance.Collider.isTrigger = true;
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
        Ball.Instance.Collider.isTrigger = true;

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
        Ball.Instance.Rb.isKinematic = false;
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


    public static void SetAnimatorRunningParameterToFalse(FiniteStateMachine fsm)
    {
        Animator animator = ((PlayerFSM)fsm).Player.Animator;
        animator.SetBool("IsRunning", false);

    }

    public static void SetAnimatorRunningParameterToTrue(FiniteStateMachine fsm)
    {
        Animator animator = ((PlayerFSM)fsm).Player.Animator;
        animator.SetBool("IsRunning", true);

    }


    public static void StopThePlayer(FiniteStateMachine fsm)
    {
        PlayerFSM playerFSM = (PlayerFSM)fsm;
        playerFSM.Player.Rigidbody.velocity = Vector3.zero;
    }



}
