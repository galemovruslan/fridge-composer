using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComposer
{
    // заполняет сетку случайными предметами

    private GridInteractor _grid;
    private List<Item> _placeableItems;
    private AvailablePlaceFinder _placeFinder;
    private bool[,] _ocupationMap;
    public ItemComposer(GridInteractor grid, List<Item> placeableItems)
    {
        _grid = grid;
        _placeableItems = placeableItems;
        _placeFinder = new AvailablePlaceFinder(grid);
        _ocupationMap = new bool[_grid.GridDescription.Rows, _grid.GridDescription.Cols];
    }

    public List<Item> Compose()
    {
        bool isComplete = false;

        while (!isComplete)
        {
            Item nextItem = GetNextItem();
        }
        return null;
    }

    private Item GetNextItem()
    {

        bool foundMacthed = false;

        while (!foundMacthed)
        {
            Item candidate = PullCandidate();
            List<Vector2Int> posiblePlaces = _placeFinder.GetAvailablePlaceIndices(candidate);

            if (posiblePlaces.Count > 0)
            {
                foundMacthed = true;

                foreach (var place in posiblePlaces)
                {

                }

            }
        }

        return null;
    }

    private Item PullCandidate()
    {
        int itemIndex = Random.Range(0, _placeableItems.Count);
        Item candidate = _placeableItems[itemIndex];
        return candidate;
    }
}
