using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Chanels/Repository")]
public class RuntimeRepository : ScriptableObject
{
    public event Action OnAdd;
    public event Action OnRemove;

    private List<GameObject> _data = new List<GameObject>();

    private void OnEnable()
    {
        _data.Clear();
    }

    public void AddObject(GameObject newObject)
    {
        if (_data.Contains(newObject)) { return; }

        _data.Add(newObject);
        OnAdd?.Invoke();
    }

    public void RemoveObject(GameObject gameObject)
    {
        if (!_data.Contains(gameObject)) { return; }

        _data.Remove(gameObject);

        if(_data.Count > 0)
        {
            OnRemove?.Invoke();
        }
    }

    public List<GameObject> GetObjects()
    {
        return _data;
        
    }

    public bool Contains(GameObject obj)
    {
        return _data.Contains(obj);
    }



}
