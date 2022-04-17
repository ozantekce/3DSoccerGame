using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperDropKickState : GoalkeeperState
{


    public static GoalkeeperDropKickState goalkeeperDropKickState = new GoalkeeperDropKickState();

    public void EnterTheState(Goalkeeper goalkeeper)
    {



        goalkeeper.ChangeCurrentAction(new DropKickAction(goalkeeper, null));

        goalkeeper.StartCurrentAction();
        goalkeeper.ChangeAnimation("DropKick");

    }

    public void ExecuteTheState(Goalkeeper goalkeeper)
    {

        if (!goalkeeper.ActionsOver())
        {
            // actionlar bitene kadar beklenir

        }
        else
        {
            goalkeeper.ChangeCurrentState(GoalkeeperIdleState.goalkeeperIdleState);
        }

    }

    public void ExitTheState(Goalkeeper goalkeeper)
    {

    }


    public class DropKickAction : GoalkeeperAction
    {

        public DropKickAction(Goalkeeper goalkeeper, GoalkeeperAction nextAction) : base(goalkeeper, nextAction, 2000f, 1500f)
        {
        }


        protected override void Action_()
        {

            Debug.Log("dropKick");
            Vector3 addVelocity
                = Goalkeeper.ShootPower * (new Vector3(0, 0.5f, Goalkeeper.Direction));

            Goalkeeper.Ball.Rb.velocity = addVelocity;
        }

        protected override void BeforeAction()
        {

            Goalkeeper.leftHand.DropBall = true;
            Goalkeeper.rightHand.DropBall = true;

        }



        protected override void AfterAction()
        {
            Goalkeeper.leftHand.DropBall = false;
            Goalkeeper.rightHand.DropBall = false;
        }


    }


}
