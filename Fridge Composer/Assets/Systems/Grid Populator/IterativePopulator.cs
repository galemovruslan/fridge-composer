using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterativePopulator : IGridPopulator
{
    private GridInteractor _grid;
    private AvailablePlaceFinder _placeFinder;

    public IterativePopulator(GridInteractor grid, AvailablePlaceFinder placeFinder)
    {
        _grid = grid;
        _placeFinder = placeFinder;
    }

    public List<PlaceableItem> PopulateGrid(List<PlaceableItem> itemsToPlace)
    {
        List<PlaceableItem> placedItems = new List<PlaceableItem>();

        foreach (var item in itemsToPlace)
        {

        }


        return placedItems;
    }
}
