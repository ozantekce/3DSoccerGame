using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBySlideState : PlayerState
{

    public static FallBySlideState fallBySlideState = new FallBySlideState();

    public void EnterTheState(Player player)
    {
        player.ChangeCurrentAction(new FallBySlideAction(player, null));
        player.StartCurrentAction();
        player.ChangeAnimation("FallBySlide");

        player.GetComponent<Collider>().isTrigger = true;
        player.GetComponent<Gravity>().GravityType_ = Gravity.GravityType.local;

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

        protected override void AfterAction()
        {
            //
        }

        protected override void BeforeAction()
        {
            Player.Rb.velocity = Vector3.zero;
        }
    }



}