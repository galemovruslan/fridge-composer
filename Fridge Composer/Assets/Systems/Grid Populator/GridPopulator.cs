using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridPopulator : MonoBehaviour
{
    public bool InFridge => _grid.InFridge;

    [SerializeField] private GridInteractor _grid;
    [SerializeField] private ItemSpawner _spawner;

    private IGridPopulator _populator;
    private List<PlaceableItem> _placedItems;

    private void Awake()
    {
        _populator = new IterativePopulator(_grid, _spawner);
    }

    public List<PlaceableItem> PopulateGrid(List<ItemDesciption> itemDesciptions, float fillRatio) // TODO заменить аргумент на коллекцию ItemDescription
    {
        _placedItems = _populator.PopulateGrid(itemDesciptions, fillRatio);
        return _placedItems;
    }

}
