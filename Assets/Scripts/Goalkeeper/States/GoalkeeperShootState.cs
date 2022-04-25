using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperShootState : GoalkeeperState
{

    public static GoalkeeperShootState goalkeeperShootState = new GoalkeeperShootState();

    public override void EnterTheState(Goalkeeper goalkeeper)
    {
        goalkeeper.ChangeCurrentAction(new ShootAction(goalkeeper, null));
        goalkeeper.StartCurrentAction();
        goalkeeper.ChangeAnimation("Shot");

    }

    public override void ExecuteTheState(Goalkeeper goalkeeper)
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

    public override void ExitTheState(Goalkeeper goalkeeper)
    {
        
    }


    public class ShootAction : GoalkeeperAction
    {

        public ShootAction(Goalkeeper player, GoalkeeperAction nextAction) : base(player, nextAction, 300, 1000f)
        {
        }


        protected override void Action_()
        {

            //Debug.Log("shoot");

            if (!Goalkeeper.BallVision.IsThereBallInVision())
                return;

            Vector3 addVelocity
                = Goalkeeper.ShootPower * (new Vector3(0, 0.5f, Goalkeeper.Direction));

            Goalkeeper.Ball.Rb.velocity = addVelocity;

        }

        protected override void BeforeAction()
        {

        }

        protected override void AfterAction()
        {
            //
        }


    }

}
