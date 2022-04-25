using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperIdleState : GoalkeeperState
{

    public static GoalkeeperIdleState goalkeeperIdleState = new GoalkeeperIdleState();

    public override void EnterTheState(Goalkeeper goalkeeper)
    {
        goalkeeper.ChangeAnimation("IdleGoalkeeper");
    }


    public override void ExecuteTheState(Goalkeeper goalkeeper)
    {


        switch (BallShooted())
        {
            case true:{GoJumpState(goalkeeper);}return;
            case false:{
                    switch (BallIsClose(goalkeeper))
                    {
                        case true:{GoGetBallState(goalkeeper);} return;
                        case false:{
                                /*
                                switch (DistanceBetweenGoalkeeperAndCenterFar(goalkeeper))
                                {
                                    case true:{GoGoWaitPositionState(goalkeeper);} return;
                                }*/
                            }break;

                    }

                }break;

        }

        Spin(goalkeeper);
        Ball ball = Ball.Instance;

        Vector3 temp = (ball.transform.position - goalkeeper.Center.transform.position).normalized;

        Vector3 target = goalkeeper.Center.position + temp * goalkeeper.radius;

        if (goalkeeper.Center.position.z > target.z)
            target.z = goalkeeper.Center.position.z;

        MyMovePosition(goalkeeper,target, goalkeeper.MovementSpeed);
        

    }

    public override void ExitTheState(Goalkeeper goalkeeper)
    {
        
    }





}
