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

    public List<Vector2Int> GetAvailablePlaceIndices(Item item)
    {
        List<Vector2Int> posiblePlaces = _grid.PosiblePlaceIndices(item);
        if (!item.IsSymetrical)
        {
            item.SwapOrientation();
            List<Vector2Int> posiblePlacesAlternative = _grid.PosiblePlaceIndices(item);
            item.SwapOrientation();
            posiblePlaces = posiblePlaces.Union<Vector2Int>(posiblePlacesAlternative).ToList();
        }

        return posiblePlaces;
    }

    public List<List<Vector2Int>> GetPosibleOcupations(Item item)
    {
        var posibleOcupations = new List<List<Vector2Int>>();
        List<Vector2Int> startingList = GetAvailablePlaceIndices(item);
        foreach (var start in startingList)
        {
            List<Vector2Int> itemOcupations = _grid.GetOcupiedIndices(start, item);
            posibleOcupations.Add(itemOcupations);
        }
        // �������� ������ ��� ������������ ���������� ��������
        return posibleOcupations;
    }

}
