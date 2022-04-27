using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AvailablePlaceFinder 
{
    GridInteractor _grid;

    public AvailablePlaceFinder(GridInteractor gridInteractor)
    {
        _grid = gridInteractor;
    }

    public List<Vector2Int> GetAvailablePlaceIndices(PlaceableItem item)
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

}
