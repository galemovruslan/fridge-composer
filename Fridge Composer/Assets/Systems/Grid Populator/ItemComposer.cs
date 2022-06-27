using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComposer
{
    // подбираем список предметов для заполнения сетки

    private GridInteractor _grid;
    private List<Item> _placeableItems;
    private AvailablePlaceFinder _placeFinder;
    private bool[,] _ocupationMap;
    private HashSet<Item> _bannedItems = new HashSet<Item>();
    private float _fillPercent;

    public ItemComposer(GridInteractor grid,  float fillPercent = 0.95f)
    {
        _fillPercent = fillPercent;
        _grid = grid;
        _placeFinder = new AvailablePlaceFinder(grid);
        _ocupationMap = new bool[_grid.GridDescription.Rows, _grid.GridDescription.Cols];
    }

    public List<PlacementDescription> Compose(List<Item> placeableItems)
    {
        _placeableItems = placeableItems;

        bool isComplete = false;
        var composerResult = new List<PlacementDescription>();

        while (!isComplete)
        {
            ItemLocationPair nextItem = GetNextItem();

            // для каждой стартовой ячейки определяем все ячейки которые будет занимать предмет
            Dictionary<Vector2Int, OcupationTestResult> startOcupationMap = GetAllOcupations(nextItem);

            // проверяем наличие наложения найденых точек с уже установленными
            LocationOrientation saveToPlacePoint = GetUnoverlapedPoint(startOcupationMap);

            // занимаем точки 
            if (saveToPlacePoint.Equals(default(LocationOrientation)))
            {
                continue;
            }
            OcupyLocations(startOcupationMap, saveToPlacePoint);

            // добавляем предмет в список
            composerResult.Add(new PlacementDescription() { 
                ComposedItem = nextItem.item,
                IsDefaulfOrient = saveToPlacePoint.isDefaultOrientation, 
                Location = saveToPlacePoint.location });

            // проверяем условие останова
            if (CheckCompletion())
            {
                isComplete = true;
            }
        }

        return composerResult; 
    }

    private bool CheckCompletion()
    {
        int rowsNumber = _ocupationMap.GetLength(0);
        int colsNumber = _ocupationMap.GetLength(1);

        int totalCells = 0;
        int ocupiedCells = 0;

        for (int i = 0; i < rowsNumber; i++)
        {
            for (int j = 0; j < colsNumber; j++)
            {
                if (_ocupationMap[i, j])
                {
                    ocupiedCells++;
                }
                totalCells++;
            }
        }
        return ((float)ocupiedCells / totalCells) >= _fillPercent;
    }

    private void OcupyLocations(Dictionary<Vector2Int, OcupationTestResult> map, LocationOrientation start)
    {
        List<Vector2Int> ocupationList = start.isDefaultOrientation ?
            map[start.location].defaultOrientationLocations :
            map[start.location].alternativeOrientationLocations;

        foreach (var item in ocupationList)
        {
            _ocupationMap[item.x, item.y] = true;
        }
    }

    private LocationOrientation GetUnoverlapedPoint(Dictionary<Vector2Int, OcupationTestResult> ocupationMap)
    {
        for (int i = 0; i < _ocupationMap.GetLength(0); i++)
        {
            for (int j = 0; j < _ocupationMap.GetLength(1); j++)
            {
                Vector2Int coordsToTest = new Vector2Int(i, j);

                if (!ocupationMap.ContainsKey(coordsToTest))
                {
                    continue;
                }
                bool overlapWithDefault = OverlapTest(ocupationMap[coordsToTest].defaultOrientationLocations);
                bool overlaoWithAlternative = OverlapTest(ocupationMap[coordsToTest].alternativeOrientationLocations);

                if (!overlapWithDefault)
                {
                    return new LocationOrientation() { isDefaultOrientation = true, location = coordsToTest };
                }
                if (!overlaoWithAlternative)
                {
                    return new LocationOrientation() { isDefaultOrientation = false, location = coordsToTest };
                }
            }
        }
        return default(LocationOrientation);
    }

    private bool OverlapTest(List<Vector2Int> coords)
    {
        foreach (var coord in coords)
        {
            if (_ocupationMap[coord.x, coord.y])
            {
                return true;
            }
        }

        return false;
    }

    private Dictionary<Vector2Int, OcupationTestResult> GetAllOcupations(ItemLocationPair nextItem)
    {
        // для каждой стартовой точки получить все занимаемые позиции при разных ориентациях
        Dictionary<Vector2Int, List<Vector2Int>> posibleItemOcupation = _placeFinder.GetPosibleOcupations(nextItem.item, nextItem.locations);
        nextItem.item.SwapOrientation();
        Dictionary<Vector2Int, List<Vector2Int>> posibleItemOcupationSwaped = _placeFinder.GetPosibleOcupations(nextItem.item, nextItem.locations);
        nextItem.item.SwapOrientation();

        // для каждой стартовой точки задаем занятые точки при обычном и повернутом положении
        var ocupationMap = new Dictionary<Vector2Int, OcupationTestResult>();
        foreach (KeyValuePair<Vector2Int, List<Vector2Int>> kvp in posibleItemOcupation)
        {
            ocupationMap[kvp.Key] = new OcupationTestResult() { defaultOrientationLocations = kvp.Value };
        }

        foreach (KeyValuePair<Vector2Int, List<Vector2Int>> kvp in posibleItemOcupationSwaped)
        {
            if (ocupationMap.ContainsKey(kvp.Key))
            {
                ocupationMap[kvp.Key].alternativeOrientationLocations = kvp.Value;
            }
            else
            {
                ocupationMap[kvp.Key] = new OcupationTestResult() { alternativeOrientationLocations = kvp.Value };
            }
        }

        return ocupationMap;
    }

    private ItemLocationPair GetNextItem()
    {
        List<Vector2Int> posibleAnchorPlaces;
        List<Vector2Int> availableUnocupiedPlaces;
        Item candidate;
        do
        {
            // выбираем случайной предмет из общего списка
            candidate = PullCandidate();
            // определяем все точки куда потенциально может встать предмет
            posibleAnchorPlaces = _placeFinder.GetAvailablePlaceIndices(candidate);
            if (posibleAnchorPlaces.Count == 0)
            {
                _bannedItems.Add(candidate);
            }
            // проверка пересечения с возможных ячеек с уже занятыми 
            Dictionary<Vector2Int, List<Vector2Int>> allPosibleOcupations = _placeFinder.GetPosibleOcupations(candidate, posibleAnchorPlaces);
            availableUnocupiedPlaces = new List<Vector2Int>();
            foreach (var anchorOcupied in allPosibleOcupations)
            {
                if (OverlapTest(anchorOcupied.Value))
                {
                    continue;
                }
                availableUnocupiedPlaces.Add(anchorOcupied.Key);
            }
        }
        while (availableUnocupiedPlaces.Count == 0);

        ItemLocationPair nextItem = new ItemLocationPair()
        {
            item = candidate,
            locations = availableUnocupiedPlaces
        };

        return nextItem;
    }

    private Item PullCandidate()
    {
        Item candidate;
        do
        {
            int itemIndex = Random.Range(0, _placeableItems.Count);
            candidate = _placeableItems[itemIndex];

        } while (_bannedItems.Contains(candidate));

        return candidate;
    }

    private struct ItemLocationPair
    {
        public Item item;
        public List<Vector2Int> locations;
    }

    private struct LocationOrientation
    {
        public Vector2Int location;
        public bool isDefaultOrientation;
    }


    private class OcupationTestResult
    {
        public List<Vector2Int> defaultOrientationLocations;
        public List<Vector2Int> alternativeOrientationLocations;
    }

    public class PlacementDescription 
    {
        public Item ComposedItem;
        public Vector2Int Location;
        public bool IsDefaulfOrient;
    }


}
