using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassState : PlayerState
{

    public static PassState passState = new PassState();

    public void Enter(Player player)
    {

        player.CurrentAction = new PassAction(player, null);
        player.CurrentAction.StartAction();
        player.ChangeAnimation("Pass");
    }

    public void Execute(Player player)
    {

        if (player.FallBySlide)
        {
            // ayaðýna kayýldý 
            player.CurrentAction.StopAction();
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if(player.CurrentAction != null)
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


    public class PassAction : PlayerAction
    {

        public PassAction(Player player, PlayerAction nextAction) : base(player, nextAction, 300f, 500f)
        {
        }


        protected override void Action_()
        {

            Debug.Log("pass");
            Player.BallVision.ballTransform = null;

            Vector3 directionVector = passTargetPosition() - Player.Ball.transform.position;
            directionVector = directionVector.normalized;

            Player.Ball.Rb.velocity = directionVector * Player.PassPower* (Player.Inputter.GetButtonPassValue() + 0.5f);


        }


        private Vector3 passTargetPosition()
        {
            //en yakýn takým arkadaþýnýn ön tarafý olacak
            return Vector3.zero;
        }


    }


}