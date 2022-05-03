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
    [SerializeField] private ItemSpawner _spawner;

    private IGridPopulator _populator;
    private List<PlaceableItem> _placedItems;

    private void Awake()
    {
        _populator = new IterativePopulator(_grid, _spawner);
    }

    public void PopulateGrid(List<ItemDesciption> itemDesciptions) // TODO заменить аргумент на коллекцию ItemDescription
    {
        _placedItems = _populator.PopulateGrid(itemDesciptions);
        _placedItems.ForEach(item => _onCreateItem.Raise(item.Item));

    }

}
