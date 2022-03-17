using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassState : PlayerState
{

    public static PassState passState = new PassState();

    public void EnterTheState(Player player)
    {

        player.ChangeCurrentAction(new PassAction(player, null));
        player.StartCurrentAction();
        player.ChangeAnimation("Pass");

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

    public void ExitTheState(Player player)
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

            Vector3 directionVector = PassTargetPosition() - Player.Ball.transform.position;
            directionVector = directionVector.normalized;

            Vector3 addVelocity
                = (Player.Inputter.GetButtonShootValue() + 0.3f) * Player.ShootPower
                * directionVector + new Vector3(0, 0.4f, 0);


            Player.Ball.Rb.velocity += addVelocity;

        }


        protected override void BeforeAction()
        {
            
        }

        protected override void AfterAction()
        {
            

        }

        private Vector3 PassTargetPosition()
        {
            //en yakýn takým arkadaþýnýn ön tarafý olacak
            return Vector3.zero;

        }


    }


}