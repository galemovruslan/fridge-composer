using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Spawners/Item", fileName ="new item spawner")]
public class ItemSpawner : ScriptableObject
{
    [SerializeField] private PlaceableItem[] _itemPool;


    private Dictionary<ItemDesciption, PlaceableItem> _spawnMap;

    private void OnEnable()
    {
        _spawnMap = new Dictionary<ItemDesciption, PlaceableItem>();

        foreach (var item in _itemPool)
        {
            _spawnMap[item.Description] = item;
        }
    }

    public PlaceableItem Spawn(ItemDesciption desciption)
    {
        if (!_spawnMap.ContainsKey(desciption))
        {
            throw new System.Exception("Unknown item decription");
        }

        PlaceableItem newItem = Instantiate(_spawnMap[desciption]);

        return newItem;
    }

}
