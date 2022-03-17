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


        // Idle için Idlewithball ve Idlewithoutball diye 2 state oluþturmak yerine 2 methoda böldüm
        // top kontrol alanýndaysa :
        if (player.BallVision.IsThereBallInVision())
        {
            
            if(currentState != State.withBall)
            {
                currentState = State.withBall;
                EnterTheState(player);
            }
            
            ExecuteWithBall(player);

        }
        // top kontrol alanýnda deðilse :
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

        //  state geçiþlerinde bazý stateler daha önceliklidir
        //  örnek olarak ayaðýna kayýldý ise diðer geçiþ kontrollerinin yapýlmasýna gerek yoktur
        //  yereDüþme > shoot > pass > koþma 
        //
        //
        if (player.FallBySlide)
        {
            // ayaðýna kayýldý 
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
        player.Rb.velocity = velocity; // -> playerýn hýzý y dýþýnda 0 a eþitlendi


    }


    private void ExecuteWithoutBall(Player player)
    {


        if (player.FallBySlide)
        {
            // ayaðýna kayýldý 
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
            // Else kadar state deðiþikliði gerekli mi diye kontrol ediliyor
            // Else de idle statein iþlemeleri yapýlýyor
            Vector3 velocity = player.Rb.velocity;
            velocity.x = 0;
            velocity.z = 0;
            player.Rb.velocity = velocity;// -> playerýn hýzý y dýþýnda 0 a eþitlendi
            //animasyon
        }


    }


}
