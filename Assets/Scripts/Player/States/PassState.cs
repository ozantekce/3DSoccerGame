using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassState : PlayerState
{

    public static PassState passState = new PassState();

    public void EnterTheState(Player player)
    {

        player.ChangeCurrentAction(new PassAction(player, null));
        player.StartCurrentAction();
        player.ChangeAnimation("Pass");

    }

    public void ExecuteTheState(Player player)
    {

        if (player.FallBySlide)
        {
            // aya��na kay�ld� 
            player.StopCurrentAction();
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if(!player.ActionsOver())
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
            // input olmad��� i�in IdleState gider
            player.ChangeCurrentState(IdleState.idleState);

        }

    }

    public void ExitTheState(Player player)
    {

    }


    public class PassAction : PlayerAction
    {

        public PassAction(Player player, PlayerAction nextAction) : base(player, nextAction, 300f, 900f)
        {
        }


        private float passButtonValue;
        private float vertcialInputValue;
        protected override void Action_()
        {

            Debug.Log("pass");
            // topun kontrol�n� b�rakt�
            Player.BallVision.ballTransform = null;

            Vector3 directionVector = PassTargetPosition() - Player.Ball.transform.position;
            directionVector = directionVector.normalized;
            Vector3 addVelocity
                = passButtonValue * Player.PassPower
                * directionVector + new Vector3(vertcialInputValue, 0.4f, 0);


            Player.Ball.Rb.velocity += addVelocity;

        }


        protected override void BeforeAction()
        {
            this.passButtonValue = Player.PassInput;
            this.vertcialInputValue = Player.VerticalInput;
        }

        protected override void AfterAction()
        {
            

        }

        
        private Vector3 PassTargetPosition()
        {
            Vector3 passTarget;

            // en yak�n tak�m arkada��n�n �n taraf� olacak
            // �uan 2 vs 2 oldu�u i�in if yeterli
            if (Player.Team == 1)
            {
                if(Player.PlayerIndex == 1)
                {
                    passTarget
                        = GameManager.Instance.teamOneList[1].GameObject_.transform.position;
                }
                else
                {
                    passTarget
                        = GameManager.Instance.teamOneList[0].GameObject_.transform.position;
                }

            }
            else
            {

                if (Player.PlayerIndex == 1)
                {
                    passTarget
                        = GameManager.Instance.teamTwoList[1].GameObject_.transform.position;
                }
                else
                {
                    passTarget
                        = GameManager.Instance.teamTwoList[0].GameObject_.transform.position;
                }

            }


            
            return passTarget;

        }


    }


}