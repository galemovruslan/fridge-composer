using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompactPopulator : IGridPopulator
{
    private GridInteractor _grid;
    private AvailablePlaceFinder _placeFinder;

    public CompactPopulator(GridInteractor grid, AvailablePlaceFinder placeFinder)
    {
        _grid = grid;
        _placeFinder = placeFinder;
    }

    public List<PlaceableItem> PopulateGrid(List<PlaceableItem> itemsToPlace)
    {
        List<OcupationVariant> itemOcupationVariantsSorted = new List<OcupationVariant>();
        List<PlaceableItem> placedItems = new List<PlaceableItem>();

        foreach (var itemPrefab in itemsToPlace)
        {
            PlaceableItem item = GameObject.Instantiate(itemPrefab);

            List<Vector2Int> posiblePlaces = _placeFinder.GetAvailablePlaceIndices(item);
            var variant = new OcupationVariant() { Item = item, PosiblePlaces = posiblePlaces };
            itemOcupationVariantsSorted.Add(variant);

            placedItems.Add(item);
        }

        itemOcupationVariantsSorted.Sort(
            (variant1, variant2) =>
            variant1.PosiblePlaces.Count.CompareTo(variant2.PosiblePlaces.Count));

        foreach (var variant in itemOcupationVariantsSorted)
        {
            PlaceItem(variant);
        }
        return placedItems;
    }

    private void PlaceItem(OcupationVariant variant)
    {
        int placeIndex = -1;
        bool placed;
        do
        {
            placeIndex++;
            placed = _grid.TryPlaceOnGrid(variant.PosiblePlaces[placeIndex], variant.Item);

            if (placed)
            {
                continue;
            }

            variant.Item.SwapOrientation();
            placed = _grid.TryPlaceOnGrid(variant.PosiblePlaces[placeIndex], variant.Item);
            if (placed)
            {
                continue;
            }
            variant.Item.SwapOrientation();
        }
        while (!placed && placeIndex < variant.PosiblePlaces.Count);
    }

}
