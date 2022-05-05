using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMethods
{


    public static void FootballerRunMethod(FiniteStateMachine fsm)
    {
        Footballer player = fsm.GetComponent<Footballer>();
        float verticalInput = player.verticalInput;
        float horizontalInput = player.horizontalInput;
        Movement.SpinAndMoveForward(player, verticalInput, horizontalInput);

    }

    public static void FootballerDribblingMethod(FiniteStateMachine fsm)
    {
        Footballer player = fsm.GetComponent<Footballer>();
        float verticalInput = player.verticalInput;
        float horizontalInput = player.horizontalInput;


        Dribbling.Dribbling_(player, verticalInput, horizontalInput);

    }


    public static void ShotMethod(FiniteStateMachine fsm)
    {

        Footballer player = fsm.GetComponent<Footballer>();
        float verticalInput = player.verticalInput;
        float horizontalInput = player.horizontalInput;
        float shotInput = player.shotInput;
        Shot.Shot_(player, shotInput, verticalInput, horizontalInput);
        

    }

    public static void PassMethod(FiniteStateMachine fsm)
    {

        Footballer player = fsm.GetComponent<Footballer>();
        float passInput = player.passInput;
        Pass.Pass_(player, passInput, player.TeamMate.transform);
        
    }

    public static void SlideMethod(FiniteStateMachine fsm)
    {

        Footballer player = fsm.GetComponent<Footballer>();
        Slide.Slide_(player, 1);
        

    }

    public static void FallMethod(FiniteStateMachine fsm)
    {

        Footballer player = fsm.GetComponent<Footballer>();
        Fall.Fall_(player);


    }



    public static void GoalkeeperTakePosition(FiniteStateMachine fsm)
    {
        Goalkeeper goalkeeper = fsm.GetComponent<Goalkeeper>();

        Animator animator = fsm.GetComponent<Animator>();

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

        Animator animator = fsm.GetComponent<Animator>();
        animator.SetTrigger("Scoop");

        fsm.GetComponent<Rigidbody>().isKinematic = true;

        Ball ball = Ball.Instance;
        ball.Rb.isKinematic = true;

        fsm.StartCoroutine(SendBallToHands(fsm,0.8f));


    }


    public static void GoalkeeperCatchBall(FiniteStateMachine fsm)
    {
        if (Ball.Instance.Rb.isKinematic)
            return;
        
        Animator animator = fsm.GetComponent<Animator>();
        animator.SetBool("Catched",true);
        
        

        Ball ball = Ball.Instance;
        ball.Rb.isKinematic = true;

        fsm.StartCoroutine(SendBallToHands2(fsm,0.1f));

    }


    public static void GoalkeeperGoToBall(FiniteStateMachine fsm)
    {
        Movable movable = fsm.GetComponent<Movable>();
        Movement.SpinToObject(movable, Ball.Instance.gameObject, 10f);
        Movement.MovePosition(movable, Ball.Instance.transform.position, 0.5f, movable.MovementSpeed);

    }



    private static IEnumerator SendBallToHands(FiniteStateMachine fsm, float duration)
    {
        Debug.Log("start");

        float T = 0;
        Ball.Instance.GetComponent<Collider>().isTrigger = true;
        while (true)
        {

            T += Time.deltaTime;
            float t01 = T / duration;

            Vector3 startPosition = Ball.Instance.transform.position;
            Vector3 endPosition = fsm.GetComponent<Goalkeeper>().RightHand.transform.position;
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

        Ball.Instance.GetComponent<Collider>().isTrigger = true;

        float T = 0;

        while (true)
        {

            T += Time.deltaTime;
            float t01 = T / duration;

            Vector3 startPosition = Ball.Instance.transform.position;
            Vector3 endPosition = fsm.GetComponent<Goalkeeper>().RightHand.transform.position;
            Vector3 pos = Vector3.Lerp(startPosition, endPosition, t01);

            Ball.Instance.transform.position = pos;

            if (t01 >= 1)
            {
                //fsm.GetComponent<Goalkeeper>().RightHand
                break;
            }

            yield return new WaitForEndOfFrame();

        }
        


    }



    public static void GoalkeeperThrowBall(FiniteStateMachine fsm)
    {


        Ball.Instance.GetComponent<Collider>().isTrigger = false;

        if(ThrowBallRunning==false)
            fsm.StartCoroutine(ThrowBall(fsm));

    }


    public static void GoalkeeperLookRivalGoalPost(FiniteStateMachine fsm)
    {
        Movement.SpinToObject(fsm.GetComponent<Movable>(), fsm.GetComponent<Goalkeeper>().GoalpostCenterRival.gameObject, 15f);
    }

    private static bool ThrowBallRunning = false;
    private static IEnumerator ThrowBall(FiniteStateMachine fsm)
    {
        Debug.Log("start");
        ThrowBallRunning = true;
        Vector3 velocity = fsm.transform.forward;
        velocity.y = 0.2f;
        Debug.Log("vel : "+velocity);
        velocity = velocity.normalized * 30f;
        int times = 0;
        
        while (times<=30)
        {
            Ball.Instance.transform.Translate(velocity*Time.deltaTime,Space.World);
            yield return new WaitForEndOfFrame();
            times++;
        }
        Debug.Log("over");
        Ball.Instance.Rb.isKinematic = false;
        yield return new WaitForEndOfFrame();
        Shot.ForceShot(fsm.GetComponent<Shotable>(), 1, 0, 0);
        ThrowBallRunning = false;
        fsm.GetComponent<Rigidbody>().isKinematic = false;
    }



    public static void GoalkeeperHoldTheBall(FiniteStateMachine fsm)
    {
        if (!Ball.Instance.GetComponent<Collider>().isTrigger)
            return;
        Goalkeeper goalkeeper = fsm.GetComponent<Goalkeeper>();
        Ball.Instance.transform.position = goalkeeper.RightHand.transform.position;

    }


}
