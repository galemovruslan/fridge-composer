
using System;
using System.Collections.Generic;
using UnityEngine;

public class FillProgress 
{

    private HashSet<Item> _spawnedItems = new HashSet<Item>();
    private HashSet<Item> _itemsOnGoals = new HashSet<Item>();
    private event Action _onComplete;
    private bool _isPaused = false;

    public void TogglePause(bool isPaused)
    {
        _isPaused = isPaused;
    }

    public void HandlePlaceItem(Item placedItem)
    {
        _itemsOnGoals.Add(placedItem);
        if (CheckIsComplete() && !_isPaused)
        {
            GameCompleted();
        }
    }

    public void HandleRemoveItem(Item item)
    {
        _itemsOnGoals.Remove(item);
    }

    public void HandleItemSpawn(Item createdItem)
    {
        _spawnedItems.Add(createdItem);
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
