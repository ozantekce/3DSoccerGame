using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperIdleState : GoalkeeperState
{

    public static GoalkeeperIdleState goalkeeperIdleState = new GoalkeeperIdleState();

    public void EnterTheState(Goalkeeper goalkeeper)
    {
        goalkeeper.ChangeAnimation("IdleGoalkeeper");
    }

    public void ExecuteTheState(Goalkeeper goalkeeper)
    {

        if (goalkeeper.Inputter.GetButtonShootValue() != 0&& goalkeeper.BallVision.IsThereBallInVision())
        {   //Shoot inputu var shootState e gider
            goalkeeper.ChangeCurrentState(GoalkeeperShootState.goalkeeperShootState);
        }
        else if (goalkeeper.Inputter.GetButtonJumpValue() != 0)
        {   //Jump inputu var jumpState e gider
            goalkeeper.ChangeCurrentState(GoalkeeperJumpState.goalkeeperJumpState);
        }
        else if (goalkeeper.Inputter.GetJoyStickVerticalValue() != 0
            || goalkeeper.Inputter.GetJoyStickHorizontalValue() != 0)
        {
            // Hareket inputu var runningState gider
            goalkeeper.ChangeCurrentState(GoalkeeperRunState.goalkeeperRunState);
        }
        // kontrol bitti 


        Vector3 velocity = goalkeeper.Rb.velocity;
        velocity.x = 0;
        velocity.z = 0;
        goalkeeper.Rb.velocity = velocity; // -> playerýn hýzý y dýþýnda 0 a eþitlendi



    }

    public void ExitTheState(Goalkeeper goalkeeper)
    {
        
    }






}
