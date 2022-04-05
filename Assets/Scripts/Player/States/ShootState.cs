using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : PlayerState
{

    public static ShootState shootState = new ShootState();

    public void EnterTheState(Player player)
    {
        player.ChangeCurrentAction(new ShootAction(player, null));
        player.StartCurrentAction();
        player.ChangeAnimation("Shoot");

    }

    public void ExecuteTheState(Player player)
    {

        if (player.FallBySlide)
        {
            // aya��na kay�ld� 
            player.StopCurrentAction();
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if(!player.ActionsOver())
        {   
            // actionlar bitene kadar beklenir

        }
        else if (player.VerticalInput != 0
            || player.HorizontalInput != 0)
        {
            // Hareket inputu var runningState gider
            player.ChangeCurrentState(RunningState.runningState);
        }
        else
        {
            // input olmad��� i�in IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }

    }

    public void ExitTheState(Player player)
    {
        
    }


    public class ShootAction : PlayerAction
    {

        public ShootAction(Player player, PlayerAction nextAction) : base(player, nextAction, 300f, 500f)
        {
        }


        private float shootButtonValue;
        private float verticalValue;
        private float horizontalValue;

        

        protected override void Action_()
        {

            //Debug.Log("shoot");

            Vector3 addVelocity
                = (shootButtonValue) * Player.ShootPower
                * (Player.transform.forward + new Vector3(verticalValue, 0.5f, horizontalValue));

            Player.Ball.Rb.velocity += addVelocity;

        }

        protected override void BeforeAction()
        {
            this.shootButtonValue = Player.ShootInput;
            this.verticalValue = Player.VerticalInput;
            this.horizontalValue = Player.HorizontalInput;
            
        }

        protected override void AfterAction()
        {
            //
        }


    }


}
