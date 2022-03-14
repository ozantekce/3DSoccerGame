using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{

    public static JumpState jumpState = new JumpState();

    public void Enter(Player player)
    {

    }

    public void Execute(Player player)
    {

        if (player.CurrentAction != null)
        {
            // actionlar bitene kadar beklenir

        }
        else if (player.Inputter.GetJoyStickVerticalValue() != 0
            || player.Inputter.GetJoyStickHorizontalValue() != 0)
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

    public void Exit(Player player)
    {

    }



}