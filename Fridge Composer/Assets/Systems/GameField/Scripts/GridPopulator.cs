using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridPopulator : MonoBehaviour
{
    [SerializeField] private GridInteractor _grid;
    [SerializeField] private List<PlaceableItem> _itemPrefabs;

    private List<OcupationVariant> _ocupationVariants = new List<OcupationVariant>();

    private void Start()
    {
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        foreach (var itemPrefab in _itemPrefabs)
        {
            PlaceableItem item = Instantiate(itemPrefab);
            List<Vector2Int> posiblePlaces = GetPlaceIndices(item);

            var variant = new OcupationVariant() { Item = item, PosiblePlaces = posiblePlaces };
            _ocupationVariants.Add(variant);
        }

        _ocupationVariants.Sort(
            (variant1, variant2) =>
            variant1.PosiblePlaces.Count.CompareTo(variant2.PosiblePlaces.Count));

        foreach (var variant in _ocupationVariants)
        {
            PlaceItem(variant);
        }

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

    private List<Vector2Int> GetPlaceIndices(PlaceableItem item)
    {
        var posiblePlaces = _grid.PosiblePlaceIndices(item.Item);
        if (!item.Item.IsSymetrical)
        {
            item.SwapOrientation();
            var posiblePlacesAlternative = _grid.PosiblePlaceIndices(item.Item);
            item.SwapOrientation();
            posiblePlaces = posiblePlaces.Union<Vector2Int>(posiblePlacesAlternative).ToList();
        }

        return posiblePlaces;
    }


    class OcupationVariant
    {
        public PlaceableItem Item;
        public List<Vector2Int> PosiblePlaces;
    }

}
