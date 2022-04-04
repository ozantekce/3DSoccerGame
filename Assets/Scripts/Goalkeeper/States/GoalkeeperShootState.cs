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



    }

    public void ExitTheState(Goalkeeper goalkeeper)
    {
        
    }


    public class ShootAction : GoalkeeperAction
    {

        public ShootAction(Goalkeeper goalkeeper, GoalkeeperAction nextAction) : base(goalkeeper, nextAction, 300f, 500f)
        {
        }


        protected override void Action_()
        {


        }

        protected override void BeforeAction()
        {
            //
        }

        protected override void AfterAction()
        {
            //
        }


    }


}
