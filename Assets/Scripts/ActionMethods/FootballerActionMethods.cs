using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerActionMethods
{



    public static void RunMethod(FiniteStateMachine fsm)
    {

        Footballer footballer = ((FootballerFSM)fsm).Footballer;

        float verticalInput = footballer.verticalInput;
        float horizontalInput = footballer.horizontalInput;
        Movement.Spin(footballer, verticalInput, horizontalInput, 15f);
        Movement.Move(footballer, verticalInput, horizontalInput);
        //Movement.SpinAndMoveForward(footballer, verticalInput, horizontalInput);

    }


    public static void Spin(FiniteStateMachine fsm)
    {
        Footballer footballer = ((FootballerFSM)fsm).Footballer;

        float verticalInput = footballer.verticalInput;
        float horizontalInput = footballer.horizontalInput;
        Movement.Spin(footballer, verticalInput, horizontalInput, 15f);
        //Movement.SpinAndMoveForward(footballer, verticalInput, horizontalInput);

    }

    public static void DribblingMethod(FiniteStateMachine fsm)
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


    public static void SetVelocityToZero(FiniteStateMachine fsm)
    {

        PlayerFSM playerFSM = (PlayerFSM)fsm;
        playerFSM.Player.Rigidbody.velocity
            = VectorCalculater.VectorZeroWithoutY(playerFSM.Player.Rigidbody.velocity);

    }


    public static void SetAnimatorRunningParameterToFalse(FiniteStateMachine fsm)
    {
        Animator animator = ((PlayerFSM)fsm).Player.Animator;
        if (animator != null)
            animator.SetBool("IsRunning", false);

    }

    public static void SetAnimatorRunningParameterToTrue(FiniteStateMachine fsm)
    {
        Animator animator = ((PlayerFSM)fsm).Player.Animator;
        animator.SetBool("IsRunning", true);

    }


    public static void SetAnimatorShotParameter(FiniteStateMachine fsm)
    {
        Animator animator = ((PlayerFSM)fsm).Player.Animator;
        animator.SetTrigger("Shot");

    }

    public static void SetAnimatorPassParameter(FiniteStateMachine fsm)
    {

        Animator animator = ((PlayerFSM)fsm).Player.Animator;
        animator.SetTrigger("Pass");

    }

    public static void SetAnimatorSlideParameter(FiniteStateMachine fsm)
    {
        Animator animator = ((PlayerFSM)fsm).Player.Animator;
        animator.SetTrigger("Slide");

    }


}
