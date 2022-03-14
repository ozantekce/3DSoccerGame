using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : PlayerState
{

    public static ShootState shootState = new ShootState();

    public void Enter(Player player)
    {

        player.CurrentAction = new ShootAction(player,null);
        player.CurrentAction.StartAction();
        player.ChangeAnimation("Shoot");

    }

    public void Execute(Player player)
    {

        if (player.FallBySlide)
        {
            // ayaðýna kayýldý 
            player.CurrentAction.StopAction();
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if(player.CurrentAction!=null)
        {   
            // actionlar bitene kadar beklenir

        }
        else if (player.Inputter.GetJoyStickVerticalValue() != 0
            || player.Inputter.GetJoyStickHorizontalValue() != 0)
        {
            // Hareket inputu var runningState gider
            player.ChangeCurrentState(RunningState.runningState);
        }
        else
        {
            // input olmadýðý için IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }

    }

    public void Exit(Player player)
    {
        
    }


    public class ShootAction : PlayerAction
    {

        public ShootAction(Player player, PlayerAction nextAction) : base(player, nextAction, 300f, 300f)
        {
        }


        protected override void Action_()
        {

            Debug.Log("shoot");
            Player.BallVision.ballTransform = null;
            Vector3 addVelocity
                = (Player.Inputter.GetButtonShootValue()+0.3f) * Player.ShootPower
                * (Player.transform.forward + new Vector3(0, 0.4f, 0));

            Player.Ball.Rb.velocity += addVelocity;


        }


    }


}
