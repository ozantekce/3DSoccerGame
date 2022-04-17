using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : PlayerState
{

    public static SlideState slideState = new SlideState();

    public void EnterTheState(Player player)
    {
        player.ChangeCurrentAction(new SlideAction(player, null));
        player.StartCurrentAction();

        player.ChangeAnimation("Slide");

    }
    public void ExecuteTheState(Player player)
    {

        if (!player.ActionsOver())
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


    public class SlideAction : PlayerAction
    {

        public SlideAction(Player player, PlayerAction nextAction) : base(player, nextAction, 100f, 1000f)
        {
        }


        protected override void Action_()
        {
            //Debug.Log("slide");
            Vector3 targetPosition = Player.transform.position + Player.transform.forward * 5f;
            
            Vector3 directionVector = targetPosition - Player.transform.position;
            directionVector = directionVector.normalized;
            directionVector.y = 0;

            Player.Rb.velocity = directionVector * Player.SlidePower;


        }

        protected override void BeforeAction()
        {
            Player.Rb.velocity = Vector3.zero;
        }

        protected override void AfterAction()
        {
            
        }


    }




}