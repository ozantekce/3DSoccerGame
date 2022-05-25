using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{


    private static List<State> ALL_STATES
    = new List<State>();


    public State()
    {
        ALL_STATES.Add(this);
    }


    /// <summary>
    /// Determines when the Action will run
    /// </summary>
    public enum RunTimeOfAction
    {
        runOnEnter,runOnPreExecution,runOnExecution,runOnPostExecution,runOnExit
    }
    /// <summary>
    /// Determines when the Transition will run
    /// </summary>
    public enum RunTimeOfTransition
    {
        runOnPreExecution,runOnExecution,runOnPostExecution
    }


    private List<MyAction> actions = new List<MyAction>();
    private List<Transition> transitions = new List<Transition>();

    private List<MyAction> enterActions = new List<MyAction>();
    private List<MyAction> exitActions = new List<MyAction>();

    private List<MyAction>  preActions = new List<MyAction>();
    private List<Transition> preTransitions = new List<Transition>();


    private List<MyAction> postActions = new List<MyAction>();
    private List<Transition> postTransitions = new List<Transition>();



    public void Enter(FiniteStateMachine fsm)
    {

        if (first)
        {
            Init();
            first = false;
        }

        EnterOptional(fsm);

        foreach (MyAction action in enterActions)
        {
            action.ExecuteAction(fsm);
        }

        


    }

    private void PreExecute(FiniteStateMachine fsm)
    {

        PreExecuteOptional(fsm);

        foreach (MyAction action in preActions)
        {

            action.ExecuteAction(fsm);

        }

        foreach (Transition transition in preTransitions)
        {

            if (transition.Decide(fsm))
            {
                //exit
                return;
            }

        }

        

    }

    public void Execute(FiniteStateMachine fsm)
    {

        PreExecute(fsm);

        ExecuteOptional(fsm);

        foreach (MyAction action in actions)
        {

            action.ExecuteAction(fsm);

        }

        foreach (Transition transition in transitions)
        {

            if (transition.Decide(fsm))
            {
                //exit
                return;
            }

        }

        

        PostExecute(fsm);

    }

    private void PostExecute(FiniteStateMachine fsm)
    {

        PostExecuteOptional(fsm);

        foreach (MyAction action in postActions)
        {

            action.ExecuteAction(fsm);

        }

        foreach (Transition transition in postTransitions)
        {

            if (transition.Decide(fsm))
            {
                //exit
                return;
            }

        }

        

    }


    public void Exit(FiniteStateMachine fsm)
    {

        foreach (MyAction action in exitActions)
        {
            action.ExecuteAction(fsm);
        }

        ExitOptional(fsm);
    }




    private bool first = true;
    public abstract void Init();



    public virtual void EnterOptional(FiniteStateMachine fsm)
    {

    }

    protected virtual void PreExecuteOptional(FiniteStateMachine fsm)
    {

    }

    protected virtual void ExecuteOptional(FiniteStateMachine fsm)
    {


    }

    protected virtual void PostExecuteOptional(FiniteStateMachine fsm)
    {

    }

    public virtual void ExitOptional(FiniteStateMachine fsm)
    {

    }







    public void AddAction(MyAction action)
    {
        actions.Add(action);
    }

    public void AddAction(MyAction action , RunTimeOfAction type)
    {
        if(type == RunTimeOfAction.runOnEnter)
        {
            enterActions.Add(action);
        }
        else if(type == RunTimeOfAction.runOnPreExecution)
        {
            preActions.Add(action);
        }
        else if (type == RunTimeOfAction.runOnExecution)
        {
            actions.Add(action);
        }
        else if (type == RunTimeOfAction.runOnPostExecution)
        {
            postActions.Add(action);
        }
        else if (type == RunTimeOfAction.runOnExit)
        {
            exitActions.Add(action);
        }

    }



    public void AddTransition(Transition transition)
    {
        transitions.Add(transition);
    }

    public void AddTransition(Transition transition, RunTimeOfTransition type)
    {
        if (type == RunTimeOfTransition.runOnPreExecution)
        {
            preTransitions.Add(transition);
        }
        else if (type == RunTimeOfTransition.runOnExecution)
        {
            transitions.Add(transition);
        }
        else if (type == RunTimeOfTransition.runOnPostExecution)
        {
            postTransitions.Add(transition);
        }

    }



    /*
    public MyAction AddAction(MyDelegates.Method method)
    {
        MyAction temp = new MyAction(method);
        actions.Add(temp);
        return temp;

    }


    public MyAction AddAction(MyDelegates.Method method,float waitBefore,float waitAfter)
    {
        MyAction temp = new MyAction(method, waitBefore, waitAfter);
        actions.Add(temp);
        return temp;
    }


    public MyAction AddAction(MyDelegates.Method method, MyDelegates.ConditionMethod condition)
    {
        MyAction temp = new MyAction(method, condition);
        actions.Add(temp);
        return temp;
    }

    public MyAction AddAction(MyDelegates.Method method, MyDelegates.ConditionMethod condition, float waitBefore, float waitAfter)
    {
        MyAction temp = new MyAction(method,condition,waitBefore,waitAfter);
        actions.Add(temp);
        return temp;

    }



    public Transition AddTransition(State state,MyDelegates.ConditionMethod condition)
    {
        Transition temp = new Transition(state,condition);
        transitions.Add(temp);
        return temp;

    }

    */

}
