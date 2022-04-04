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
        else if (goalkeeper.Inputter.GetJoyStickVerticalValue() != 0
            || goalkeeper.Inputter.GetJoyStickHorizontalValue() != 0)
        {
            // Hareket inputu var runningState gider
            goalkeeper.ChangeCurrentState(GoalkeeperRunState.goalkeeperRunState);
        }
        else
        {
            // input olmadýðý için IdleState gider
            goalkeeper.ChangeCurrentState(GoalkeeperIdleState.goalkeeperIdleState);

        }

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

            //Debug.Log("shoot");

            Vector3 addVelocity
                = (Goalkeeper.Inputter.GetButtonShootValue() + 0.3f) * Goalkeeper.ShootPower
                * (Goalkeeper.transform.forward + new Vector3(0, 0.4f, 0));


            Goalkeeper.Ball.Rb.velocity += addVelocity;
            // top dönsün
            Goalkeeper.Ball.Rb.angularVelocity = Vector3.left * 100f;

            Goalkeeper.Inputter.SetButtonShootValue(0);

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
