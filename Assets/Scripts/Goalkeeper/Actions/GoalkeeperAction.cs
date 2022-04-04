using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoalkeeperAction
{

    private bool actionIsOver;
    private float waitBeforeAction;
    private float waitAfterAction;
    private Goalkeeper goalkeeper;
    private GoalkeeperAction nextAction;


    public bool ActionIsOver { get => actionIsOver; set => actionIsOver = value; }
    public float WaitBeforeAction { get => waitBeforeAction; set => waitBeforeAction = value; }
    public float WaitAfterAction { get => waitAfterAction; set => waitAfterAction = value; }
    public Goalkeeper Goalkeeper { get => goalkeeper; set => goalkeeper = value; }
    public GoalkeeperAction NextAction { get => nextAction; }

    public void AddAction(GoalkeeperAction action)
    {
        GoalkeeperAction temp = this;
        while (temp.nextAction != null)
        {
            temp = temp.nextAction;
        }
        temp.nextAction = action;

    }


    public GoalkeeperAction(Goalkeeper goalkeeper, GoalkeeperAction nextAction, float waitBeforeAction, float waitAfterAction)
    {
        this.waitBeforeAction = waitBeforeAction / 1000f;
        this.waitAfterAction = waitAfterAction / 1000f;
        this.goalkeeper = goalkeeper;
        this.nextAction = nextAction;

    }

    public void StartAction()
    {
        if (!actionIsOver)
            goalkeeper.StartCoroutine(Action());
    }

    public void StopAction()
    {

        goalkeeper.StopCoroutine(Action());

    }

    protected abstract void Action_();

    protected abstract void BeforeAction();
    protected abstract void AfterAction();

    private IEnumerator Action()
    {

        actionIsOver = false;
        BeforeAction();
        yield return new WaitForSeconds(waitBeforeAction);

        Action_();

        yield return new WaitForSeconds(waitAfterAction);
        actionIsOver = true;
        AfterAction();

        goalkeeper.MoveNextAction();

    }




}
