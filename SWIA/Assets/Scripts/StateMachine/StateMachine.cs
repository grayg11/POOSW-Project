﻿using UnityEngine;
using System.Collections;
public class StateMachine : MonoBehaviour
{
    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }

    public virtual State PreviousState
    {
        get { return _previousState; ; }
        set { _previousState = value; }
    }

    protected State _currentState;
    protected State _previousState;
    protected bool _inTransition;

    public virtual T GetState<T>() where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
            target = gameObject.AddComponent<T>();
        return target;
    }

    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }
    protected virtual void Transition(State value)
    {
        if (_currentState == value || _inTransition)
            return;
        _inTransition = true;

        if (_currentState != null)
            _currentState.Exit();

        _previousState = _currentState;
        _currentState = value;

        if (_currentState != null)
            _currentState.Enter();

        _inTransition = false;
    }
}