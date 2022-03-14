using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAction
{
    
    private bool actionIsOver;
    private float waitBeforeAction;
    private float waitAfterAction;
    private Player player;
    private PlayerAction nextAction;


    public bool ActionIsOver { get => actionIsOver; set => actionIsOver = value; }
    public float WaitBeforeAction { get => waitBeforeAction; set => waitBeforeAction = value; }
    public float WaitAfterAction { get => waitAfterAction; set => waitAfterAction = value; }
    public Player Player { get => player; set => player = value; }

    public PlayerAction(Player player,PlayerAction nextAction, float waitBeforeAction, float waitAfterAction)
    {
        this.waitBeforeAction = waitBeforeAction/1000f;
        this.waitAfterAction = waitAfterAction/1000f;
        this.player = player;
        this.nextAction = nextAction;

    }

    public void StartAction()
    {
        if(!actionIsOver)
            player.StartCoroutine(Action());
    }

    public void StopAction()
    {

        player.StopCoroutine(Action());

    }

    protected abstract void Action_();

    private IEnumerator Action()
    {

        actionIsOver = false;

        yield return new WaitForSeconds(waitBeforeAction);

        Action_();

        yield return new WaitForSeconds(waitAfterAction);

        actionIsOver = true;
        player.CurrentAction = nextAction;
        
        if(nextAction != null)
            player.CurrentAction.StartAction();

    }
}
