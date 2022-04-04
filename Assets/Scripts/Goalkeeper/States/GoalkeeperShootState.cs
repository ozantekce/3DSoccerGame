using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperShootState : GoalkeeperState
{

    public static GoalkeeperShootState goalkeeperShootState = new GoalkeeperShootState();

    public void EnterTheState(Goalkeeper goalkeeper)
    {
        goalkeeper.ChangeCurrentAction(new ShootAction(goalkeeper, null));
        goalkeeper.StartCurrentAction();
        goalkeeper.ChangeAnimation("Shoot");

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


    public class ShootAction : GoalkeeperAction
    {

        public ShootAction(Goalkeeper player, GoalkeeperAction nextAction) : base(player, nextAction, 0, 1000f)
        {
        }


        protected override void Action_()
        {

            //Debug.Log("shoot");

            Vector3 addVelocity
                = Goalkeeper.ShootPower * (new Vector3(0, 0.5f, 0.5f));

            Goalkeeper.Ball.Rb.velocity = addVelocity; ;

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
