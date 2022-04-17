using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperOverhandThrowState : GoalkeeperState
{


    public static GoalkeeperOverhandThrowState goalkeeperOverhandThrowState = new GoalkeeperOverhandThrowState();

    public void EnterTheState(Goalkeeper goalkeeper)
    {



        goalkeeper.ChangeCurrentAction(new WaitAction(goalkeeper, new OverhandThrowAction(goalkeeper, null)));

        goalkeeper.StartCurrentAction();
        goalkeeper.ChangeAnimation("OverhandThrow");

    }

    public void ExecuteTheState(Goalkeeper goalkeeper)
    {

        if (!goalkeeper.ActionsOver())
        {
            // actionlar bitene kadar beklenir
            goalkeeper.Rb.velocity = Vector3.zero;
        }
        else
        {
            goalkeeper.ChangeCurrentState(GoalkeeperIdleState.goalkeeperIdleState);
        }

    }

    public void ExitTheState(Goalkeeper goalkeeper)
    {

    }

    public class WaitAction : GoalkeeperAction
    {

        public WaitAction(Goalkeeper goalkeeper, GoalkeeperAction nextAction) : base(goalkeeper, nextAction, 0f, 1500f)
        {
        }


        protected override void Action_()
        {


        }

        protected override void BeforeAction()
        {
            Goalkeeper.leftHand.DropBall = true;

        }



        protected override void AfterAction()
        {

        }


    }

    public class OverhandThrowAction : GoalkeeperAction
    {

        public OverhandThrowAction(Goalkeeper goalkeeper, GoalkeeperAction nextAction) : base(goalkeeper, nextAction, 100f, 1500f)
        {
        }


        protected override void Action_()
        {


            Debug.Log("OverhandThrowState");
            Vector3 addVelocity
                = Goalkeeper.ShootPower * (new Vector3(0, 0.5f, Goalkeeper.Direction));

            Goalkeeper.Ball.Rb.velocity = addVelocity;
        }

        protected override void BeforeAction()
        {

            //Goalkeeper.leftHand.DropBall = true;
            Goalkeeper.rightHand.DropBall = true;

        }



        protected override void AfterAction()
        {

        }


    }


}
