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
        player.ChangeAnimation("Shot");

    }

    public void ExecuteTheState(Player player)
    {

        if (player.FallBySlide)
        {
            // ayaðýna kayýldý 
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
            // input olmadýðý için IdleState gider
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
        protected override void Action_()
        {

            Debug.Log("pass");
            Transform target = PassTargetTransform();

            Vector3 directionVector = target.position - Player.Ball.transform.position;
            directionVector = directionVector.normalized;
            Vector3 addVelocity
                = passButtonValue * Player.PassPower
                * (directionVector + new Vector3(0, 0.1f, 0));

            if(VectorCalculater.CheckVectorXFrontOfVectorY(
                    Player.transform.forward,
                    (target.position - Player.transform.position).normalized
                ))
            {
                Player.Ball.Rb.velocity += addVelocity;
            }
            else
            {
                Player.Ball.Rb.velocity += Player.transform.forward*addVelocity.magnitude/2;
            }


            

        }


        protected override void BeforeAction()
        {
            this.passButtonValue = Player.PassInput;
        }

        protected override void AfterAction()
        {
            

        }

        
        private Transform PassTargetTransform()
        {
            Transform passTarget;

            // en yakýn takým arkadaþýnýn ön tarafý olacak
            // þuan 2 vs 2 olduðu için if yeterli
            if (Player.Team == 1)
            {
                if(Player.PlayerIndex == 1)
                {
                    passTarget
                        = GameManager.Instance.teamOneList[1].GameObject_.transform;
                }
                else
                {
                    passTarget
                        = GameManager.Instance.teamOneList[0].GameObject_.transform;
                }

            }
            else
            {

                if (Player.PlayerIndex == 1)
                {
                    passTarget
                        = GameManager.Instance.teamTwoList[1].GameObject_.transform;
                }
                else
                {
                    passTarget
                        = GameManager.Instance.teamTwoList[0].GameObject_.transform;
                }

            }


            
            return passTarget;

        }


    }


}