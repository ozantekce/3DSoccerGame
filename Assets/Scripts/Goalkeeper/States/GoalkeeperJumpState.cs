using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperJumpState : GoalkeeperState
{

    public static GoalkeeperJumpState goalkeeperJumpState = new GoalkeeperJumpState();


    public void EnterTheState(Goalkeeper goalkeeper)
    {
        goalkeeper.ChangeCurrentAction(new JumpAction(goalkeeper
            , new WaitForStandUpAction(goalkeeper,null)));

        goalkeeper.StartCurrentAction();


    }

    public void ExecuteTheState(Goalkeeper goalkeeper)
    {

        if(goalkeeper.CurrentAction == null)
        {
            goalkeeper.ChangeCurrentState(GoalkeeperIdleState.goalkeeperIdleState);
        }

    }

    public void ExitTheState(Goalkeeper goalkeeper)
    {
        
    }

    public static Vector3 jumpVelocity;

    public class JumpAction : GoalkeeperAction
    {

        public JumpAction(Goalkeeper goalkeeper, GoalkeeperAction nextAction) : base(goalkeeper, nextAction, 300f, 1500f)
        {
        }


        protected override void Action_()
        {

            Debug.Log("jump");
        }

        protected override void BeforeAction()
        {
            
            Goalkeeper.Rb.velocity = jumpVelocity;
            if (jumpVelocity.x == 0)
                Goalkeeper.ChangeAnimation("Jump");
            else if (jumpVelocity.x > 0)
                Goalkeeper.ChangeAnimation("JumpRight");
            else
                Goalkeeper.ChangeAnimation("JumpLeft");

        }





        protected override void AfterAction()
        {
            //
            
        }


    }

    public class WaitForStandUpAction : GoalkeeperAction
    {

        public WaitForStandUpAction(Goalkeeper goalkeeper, GoalkeeperAction nextAction) : base(goalkeeper, nextAction, 0, 1500f)
        {
        }


        protected override void Action_()
        {

            Debug.Log("stand up");
            Vector3 velocity = Goalkeeper.Rb.velocity;
            velocity.x = 0;
            velocity.z = 0;
            Goalkeeper.Rb.velocity = velocity;

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
