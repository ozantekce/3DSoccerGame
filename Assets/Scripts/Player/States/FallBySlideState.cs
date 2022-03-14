using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBySlideState : PlayerState
{

    public static FallBySlideState fallBySlideState = new FallBySlideState();

    public void Enter(Player player)
    {
        player.CurrentAction = new FallBySlideAction(player, null);
        player.CurrentAction.StartAction();
        player.ChangeAnimation("FallBySlide");
        player.GetComponent<Collider>().isTrigger = true;
        player.GetComponent<Gravity>().GravityType_ = Gravity.GravityType.local;
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

        player.FallBySlide = false;
        player.GetComponent<Collider>().isTrigger = false;
        player.GetComponent<Gravity>().GravityType_ = Gravity.GravityType.global;

    }


    public class FallBySlideAction : PlayerAction
    {

        public FallBySlideAction(Player player, PlayerAction nextAction) : base(player, nextAction, 1000f, 1000f)
        {
        }


        protected override void Action_()
        {
            //Debug.Log(" FallBySlideAction ");


        }


    }



}