using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : PlayerState
{

    public static SlideState slideState = new SlideState();

    public void Enter(Player player)
    {

        player.CurrentAction = new SlideAction(player, null);
        player.CurrentAction.StartAction();
        player.ChangeAnimation("Slide");
    }

    public void Execute(Player player)
    {

        if (player.CurrentAction != null)
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


    public class SlideAction : PlayerAction
    {

        public SlideAction(Player player, PlayerAction nextAction) : base(player, nextAction, 300f, 1000f)
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





    }


}