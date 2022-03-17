using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState: PlayerState
{
    public static IdleState idleState = new IdleState();

    private State currentState = State.withoutBall;
    private enum State
    {
        withBall, withoutBall
    }

    public void EnterTheState(Player player)
    {

        if(currentState == State.withoutBall)
        {
            //Debug.Log("enter : IdleState_withoutBall");
            player.ChangeAnimation("Idle");
        }
        else
        {
            //Debug.Log("enter : IdleState_withBall");
            player.ChangeAnimation("IdleWithBallRight");
        }




    }

    public void ExecuteTheState(Player player)
    {


        // Idle i�in Idlewithball ve Idlewithoutball diye 2 state olu�turmak yerine 2 methoda b�ld�m
        // top kontrol alan�ndaysa :
        if (player.BallVision.IsThereBallInVision())
        {
            
            if(currentState != State.withBall)
            {
                currentState = State.withBall;
                EnterTheState(player);
            }
            
            ExecuteWithBall(player);

        }
        // top kontrol alan�nda de�ilse :
        else
        {
            
            if (currentState != State.withoutBall)
            {
                currentState = State.withoutBall;
                EnterTheState(player);
            }
            ExecuteWithoutBall(player);

        }

    }

    public void ExitTheState(Player player)
    {
        if (currentState == State.withoutBall)
        {
            //Debug.Log("exit : IdleState_withoutBall");
            player.ChangeAnimation("Idle");
        }
        else
        {
            //Debug.Log("exit : IdleState_withBall");
            player.ChangeAnimation("IdleWithBallRight");
        }
    }

    private void ExecuteWithBall(Player player)
    {

        //  state ge�i�lerinde baz� stateler daha �nceliklidir
        //  �rnek olarak aya��na kay�ld� ise di�er ge�i� kontrollerinin yap�lmas�na gerek yoktur
        //  yereD��me > shoot > pass > ko�ma 
        //
        //
        if (player.FallBySlide)
        {
            // aya��na kay�ld� 
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if (player.Inputter.GetButtonShootValue()!=0)
        {   //Shoot inputu var shootState e gider
            player.ChangeCurrentState(ShootState.shootState);
        }
        else if(player.Inputter.GetButtonPassValue()!=0)
        {   //Pass inputu var passState e gider
            player.ChangeCurrentState(PassState.passState);
        }
        else if (player.Inputter.GetJoyStickVerticalValue() != 0
            || player.Inputter.GetJoyStickHorizontalValue() != 0)
        {
            // Hareket inputu var runningState gider
            player.ChangeCurrentState(RunningState.runningState);
        }
        // kontrol bitti 


        Vector3 velocity = player.Rb.velocity;
        velocity.x = 0;
        velocity.z = 0;
        player.Rb.velocity = velocity; // -> player�n h�z� y d���nda 0 a e�itlendi


    }


    private void ExecuteWithoutBall(Player player)
    {


        if (player.FallBySlide)
        {
            // aya��na kay�ld� 
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if (player.Inputter.GetButtonSlideValue()!=0)
        {   
            //Slide inputu var slideState e gider
            player.ChangeCurrentState(SlideState.slideState);

        }
        else if (player.Inputter.GetJoyStickVerticalValue() != 0
            || player.Inputter.GetJoyStickHorizontalValue() != 0)
        {   // Hareket inputu var runningState gider
            player.ChangeCurrentState(RunningState.runningState);
        }
        else
        {
            // Else kadar state de�i�ikli�i gerekli mi diye kontrol ediliyor
            // Else de idle statein i�lemeleri yap�l�yor
            Vector3 velocity = player.Rb.velocity;
            velocity.x = 0;
            velocity.z = 0;
            player.Rb.velocity = velocity;// -> player�n h�z� y d���nda 0 a e�itlendi
            //animasyon
        }


    }


}
