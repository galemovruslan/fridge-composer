using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class RuntimeSet<T> : ScriptableObject
{
    public event Action OnAdd;
    public event Action OnRemove;

    private List<T> _data = new List<T>();

    private void OnEnable()
    {
        _data.Clear();
    }

    public void AddObject(T newObject)
    {
        if (_data.Contains(newObject)) { return; }

        _data.Add(newObject);
        OnAdd?.Invoke();
    }

    public void RemoveObject(T gameObject)
    {
        if (!_data.Contains(gameObject)) { return; }

        _data.Remove(gameObject);

        if(_data.Count > 0)
        {
            OnRemove?.Invoke();
        }
    }

    public List<T> GetObjects()
    {
        return _data;
        
    }

    public bool Contains(T obj)
    {
        return _data.Contains(obj);
    }



}
