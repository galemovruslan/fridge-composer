using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemReplacer
{

    public ItemReplacer()
    {
    }

    public void Replace(IEnumerable<GridInteractor> recievingGrids, IEnumerable<PlaceableItem> itemsToReplace, float ratioOfReplace = 0.5f)
    {

        var selectedItems = SelectItemsToReplace(itemsToReplace, ratioOfReplace);
        MoveSelected(recievingGrids, selectedItems);
    }

    private void MoveSelected(IEnumerable<GridInteractor> recievingGrids, IEnumerable<PlaceableItem> selectedItems)
    {
        foreach (var item in selectedItems)
        {
            var grid = PickGrid(recievingGrids);
            MoveToGrid(grid, item);
        }
    }

    private void MoveToGrid(GridInteractor grid, PlaceableItem item, int maxIterations = 20)
    {

        AvailablePlaceFinder _placeFinder = new AvailablePlaceFinder(grid);
        List<Vector2Int> placeCandidates = _placeFinder.GetAvailablePlaceIndices(item.Item);
        if (placeCandidates.Count == 0)
        {
            return;
        }

        bool isPlaced;
        int iteration = 0;

        do
        {
            int pointIndex = Random.Range(0, placeCandidates.Count);
            var selectedPoint = placeCandidates[pointIndex];

            isPlaced = grid.TryPlaceOnGrid(selectedPoint, item);
            if (!isPlaced)
            {
                item.SwapOrientation();
                isPlaced = grid.TryPlaceOnGrid(placeCandidates[pointIndex], item);
            }
            iteration++;
        }
        while (!isPlaced && iteration < maxIterations );
    }

    private GridInteractor PickGrid(IEnumerable<GridInteractor> recievingGrids)
    {
        var gridIndexPairs = recievingGrids
            .Select((grid, index) => new { Grid = grid, Index = index })
            .OrderByDescending(t => t.Index);

        int gridCount = gridIndexPairs.Select(t => t.Index).First() + 1;
        int pickIndex = Random.Range(0, gridCount);

        return gridIndexPairs
            .Where(t => t.Index == pickIndex)
            .Select(t => t.Grid)
            .First();
    }

    private IEnumerable<PlaceableItem> SelectItemsToReplace(IEnumerable<PlaceableItem> selectFrom, float selectionRatio)
    {
        return selectFrom.Where(t => { return Random.Range(0f, 1f) <= selectionRatio; });
    }


}
