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
        float inputVertical = -goalkeeper.Inputter.GetJoyStickVerticalValue();
        if (inputVertical==0)
            goalkeeper.ChangeAnimation("Jump");
        else if(inputVertical>0)
            goalkeeper.ChangeAnimation("JumpRight");
        else
            goalkeeper.ChangeAnimation("JumpLeft");

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


    public class JumpAction : GoalkeeperAction
    {

        public JumpAction(Goalkeeper goalkeeper, GoalkeeperAction nextAction) : base(goalkeeper, nextAction, 300f, 1500f)
        {
        }


        protected override void Action_()
        {

            Debug.Log("jump");
            float inputVertical = -Goalkeeper.Inputter.GetJoyStickVerticalValue();
            Debug.Log(inputVertical);
            Goalkeeper.Rb.velocity = (new Vector3(inputVertical, 0.7f, 0f)) * Goalkeeper.JumpPower;
            

            Goalkeeper.Inputter.SetButtonJumpValue(0);
            Goalkeeper.Inputter.SetVerticalValue(0);
            Goalkeeper.Inputter.SetHorizontalValue(0);


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
