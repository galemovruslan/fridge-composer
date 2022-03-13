using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSet _grids;

    private HashSet<Item> _spawnedItems = new HashSet<Item>();
    private HashSet<Item> _itemsOnGoals = new HashSet<Item>();

    private void OnEnable()
    {
        GridPopulator.OnCreateItem += HandleItemSpawn;
        GridInteractor.OnPlaceItem += HandlePlaceItem;
        GridInteractor.OnRemoveItem += HandleRemoveItem;
    }
    private void OnDisable()
    {
        GridPopulator.OnCreateItem -= HandleItemSpawn;
        GridInteractor.OnPlaceItem -= HandlePlaceItem;
        GridInteractor.OnRemoveItem -= HandleRemoveItem;

    }

    private void HandlePlaceItem(Item placedItem)
    {
        _itemsOnGoals.Add(placedItem);
        if (CheckIsComplete())
        {
            GameCompleted();
        }
    }

    private void HandleRemoveItem(Item item)
    {
        _itemsOnGoals.Remove(item);
    }

    private void HandleItemSpawn(Item createdItem)
    {
        _spawnedItems.Add(createdItem);
    }

    private bool CheckIsComplete()
    {
        return _spawnedItems.IsSubsetOf(_itemsOnGoals);
    }

    private void GameCompleted()
    {
        Debug.Log("You Win");
    }
}
