using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotState : PlayerState
{

    public static ShotState shootState = new ShotState();

    public void EnterTheState(Player player)
    {
        player.ChangeCurrentAction(new ShotAction(player, null));
        player.StartCurrentAction();
        player.ChangeAnimation("Shot");

    }

    public void ExecuteTheState(Player player)
    {

        if (player.FallBySlide)
        {
            // ayaðýna kayýldý 
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
            // input olmadýðý için IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }

    }

    public void ExitTheState(Player player)
    {
        
    }


    public class ShotAction : PlayerAction
    {

        public ShotAction(Player player, PlayerAction nextAction) : base(player, nextAction, 300f, 500f)
        {
        }


        private float shootButtonValue;
        private float verticalValue;
        private float horizontalValue;


        protected override void Action_()
        {
            //Debug.Log("shot");
            Vector3 shotVector
                = (shootButtonValue) * Player.ShootPower
                * (Player.transform.forward + new Vector3(verticalValue, 0.5f, horizontalValue));
            Ball.Instance.Shot(shotVector);

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
