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
        
        Vector3 localDir
            = Quaternion.Inverse(goalkeeper.transform.rotation) * (targetPos - goalkeeper.transform.position);

        bool isForward = localDir.z > 0;
        bool isUp = localDir.y > 0;
        bool isRight = localDir.x > 0;

        if (isRight)
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

        fsm.StartCoroutine(SendBallToHands(fsm, Ball.Instance.transform.position));


    }

    public static void GoalkeeperGoToBall(FiniteStateMachine fsm)
    {
        Movable movable = fsm.GetComponent<Movable>();
        Movement.SpinToObject(movable, Ball.Instance.gameObject, 10f);
        Movement.MovePosition(movable, Ball.Instance.transform.position, 0.5f, movable.MovementSpeed);

    }



    private static IEnumerator SendBallToHands(FiniteStateMachine fsm, Vector3 startPosition)
    {
        Debug.Log("start");

        float T = 0;

        while (true)
        {

            T += Time.deltaTime;
            float duration = 0.8f;
            float t01 = T / duration;

            Vector3 A = startPosition;
            Vector3 B = fsm.GetComponent<Goalkeeper>().RightHand.transform.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            Ball.Instance.transform.position = pos;

            if (t01 >= 1)
            {
                //fsm.GetComponent<Goalkeeper>().RightHand
                Ball.Instance.transform.SetParent(fsm.GetComponent<Goalkeeper>().RightHand.transform);
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


    public static void EmptyMethod(FiniteStateMachine fsm)
    {

    }



}
