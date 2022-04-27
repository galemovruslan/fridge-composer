using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridPopulator : MonoBehaviour
{
    public bool InFridge => _grid.InFridge;

    [SerializeField] private ItemGameEvent _onCreateItem;
    [SerializeField] private GridInteractor _grid;
    [SerializeField] private List<PlaceableItem> _itemPrefabs;

    private AvailablePlaceFinder _placeFinder;
    private IGridPopulator _populator;

    private void Awake()
    {
        _placeFinder = new AvailablePlaceFinder(_grid);
        _populator = new CompactPopulator(_grid, _placeFinder);
    }


    private void Start()
    {
        PopulateGrid(_itemPrefabs);
    }


    public void PopulateGrid(List<PlaceableItem> itemPrefabs)
    {
        List<PlaceableItem> spawnedItems =_populator.PopulateGrid(itemPrefabs);
        spawnedItems.ForEach(item => _onCreateItem.Raise(item.Item));

    }

}
