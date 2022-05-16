using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemReplacer
{

    public ItemReplacer()
    {
    }

    public void Replace(
        IEnumerable<GridInteractor> sourceGrids,
        IEnumerable<GridInteractor> recievingGrids,
        IEnumerable<PlaceableItem> itemsToReplace,
        float ratioOfReplace = 0.5f)
    {

        var selectedItems = SelectItemsToReplace(itemsToReplace, ratioOfReplace);
        MoveSelected(sourceGrids, recievingGrids, selectedItems);
    }

    private void MoveSelected(
        IEnumerable<GridInteractor> sourcegGrids,
        IEnumerable<GridInteractor> recievingGrids,
        IEnumerable<PlaceableItem> selectedItems)
    {
        foreach (var item in selectedItems)
        {
            var grid = PickGrid(recievingGrids);
            RemoveFromGrid(item, sourcegGrids);
            MoveToGrid(grid, item);
        }
    }

    private void MoveToGrid(GridInteractor toGrid, PlaceableItem item, int maxIterations = 20)
    {

        AvailablePlaceFinder _placeFinder = new AvailablePlaceFinder(toGrid);
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

            isPlaced = toGrid.TryPlaceOnGrid(selectedPoint, item);
            if (!isPlaced)
            {
                item.SwapOrientation();
                isPlaced = toGrid.TryPlaceOnGrid(placeCandidates[pointIndex], item);
            }
            iteration++;
        }
        while (!isPlaced && iteration < maxIterations);
    }

    private GridInteractor PickGrid(IEnumerable<GridInteractor> recievingGrids)
    {
        var gridIndexPairs = recievingGrids
            .Select((grid, index) => new { Grid = grid, Index = index })
            .OrderByDescending(t => t.Index);

        int gridCount = gridIndexPairs.Count();
        int pickIndex = Random.Range(0, gridCount);

        return gridIndexPairs
            .Where(t => t.Index == pickIndex)
            .Select(t => t.Grid)
            .First();
    }

    private IEnumerable<PlaceableItem> SelectItemsToReplace(IEnumerable<PlaceableItem> selectFrom, float selectionRatio)
    {
        //var selected = selectFrom.Where(t => { return Random.Range(0f, 1f) <= selectionRatio; });

        //var selectedLength = selected.Count();
        //if (selectedLength == 0)
        //{

        //}
        int requredSelectionLength = Mathf.FloorToInt(selectFrom.Count() * selectionRatio);

        requredSelectionLength = (requredSelectionLength == 0) ? 1 : requredSelectionLength;

        var selected = selectFrom.OrderBy(t =>
            {
                System.Random rnd = new System.Random();
                return rnd.Next();
            })
                .Take(requredSelectionLength);
        return selected;
    }

    private void RemoveFromGrid(PlaceableItem item, IEnumerable<GridInteractor> sourcegGrids)
    {
        foreach (var grid in sourcegGrids)
        {
            grid.RemoveFromGrid(item.Item);
        }
    }

}
