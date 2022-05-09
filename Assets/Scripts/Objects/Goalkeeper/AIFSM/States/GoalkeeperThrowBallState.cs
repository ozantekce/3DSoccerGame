using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperThrowBallState : State
{

    private readonly static GoalkeeperThrowBallState instance = new GoalkeeperThrowBallState();
    public static GoalkeeperThrowBallState Instance => instance;
    private GoalkeeperThrowBallState()
    {


    }

    MyAction throwTheBall;
    public override void Init()
    {

        throwTheBall = new MyAction(GoalkeeperActionMethods.ThrowBall, 1.6f, 2f);

        //  TRANSITIONS
        //  #1
        AddTransition(new Transition(GoalkeeperIdleState.Instance, (fsm) =>
        {
            return throwTheBall.ActionOver(fsm);
        }
        ));


        //  ACTIONS
        //  #1
        AddAction(throwTheBall
            , RunTimeOfAction.runOnEnter);

        //  #2
        AddAction(new MyAction(GoalkeeperActionMethods.HoldTheBall)
            ,RunTimeOfAction.runOnPreExecution);

        //  #1:OnEnter
        AddAction(new MyAction(GoalkeeperActionMethods.PlayThrowAnim)
            , RunTimeOfAction.runOnEnter);



    }





}
