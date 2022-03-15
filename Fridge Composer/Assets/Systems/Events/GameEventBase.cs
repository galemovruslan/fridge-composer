using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventBase<T> : ScriptableObject
{
    private List<Action<T>> _listeners = new List<Action<T>>();

    private void OnEnable()
    {
        _listeners.Add(value => { });
    }

    public void Raise(T value)
    {
        _listeners.ForEach((action) => action.Invoke(value));
    }

    public void AddListener(Action<T> action)
    {
        if (_listeners.Contains(action))
        {
            return;
        }
        _listeners.Add(action);
    }

    public void RemoveListener(Action<T> action)
    {
        if (_listeners.Contains(action))
        {
            _listeners.Remove(action);
        }
    }

}
