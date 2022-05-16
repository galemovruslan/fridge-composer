
using System;
using System.Collections.Generic;
using UnityEngine;

public class FillProgress 
{

    private HashSet<Item> _spawnedItems = new HashSet<Item>();
    private HashSet<Item> _itemsOnGoals = new HashSet<Item>();
    private event Action _onComplete;
    private bool _isPaused = false;
    private bool _isChecking = false;

    public void TogglePause(bool isPaused)
    {
        _isPaused = isPaused;
    }

    public void Start()
    {
        _isChecking = true;
    }

    public void Reset()
    {
        _isPaused = false;
        _isChecking = false;
        _spawnedItems = new HashSet<Item>();
        _itemsOnGoals = new HashSet<Item>();
    }

    public void HandlePlaceItem(Item placedItem)
    {
        _itemsOnGoals.Add(placedItem);
        if (CheckIsComplete() && !_isPaused && _isChecking)
        {
            GameCompleted();
        }
        Debug.Log($"Items spawned: {_spawnedItems.Count}, items on goal: {_itemsOnGoals.Count}");
    }

    public void HandleRemoveItem(Item item)
    {
        _itemsOnGoals.Remove(item);
        Debug.Log($"Items spawned: {_spawnedItems.Count}, items on goal: {_itemsOnGoals.Count}");
    }

    public void HandleItemSpawn(Item createdItem)
    {
        _spawnedItems.Add(createdItem);
        Debug.Log($"Items spawned: {_spawnedItems.Count}");
    }

    public void AddListener( Action action)
    {
        _onComplete += action;
    }

    public void RemoveListener(Action action)
    {
        _onComplete -= action;
    }

    private bool CheckIsComplete()
    {
        return _spawnedItems.IsSubsetOf(_itemsOnGoals);
    }

    private void GameCompleted()
    {
        _onComplete?.Invoke();
    }

    
}
