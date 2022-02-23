using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int _rowsNumber;
    private int _colsNumber;
    private float _cellWidth;
    private Vector3 _offset;
    private Cell[,] _cells;
    private Dictionary<Item, List<Vector2Int>> _gridIndicesMap;


    public Grid(int rows, int cols, int cellWidth, Vector3 offset, SurfaceDescription cellAttributes)
    {
        _offset = offset;
        _cellWidth = cellWidth;
        _rowsNumber = rows;
        _colsNumber = cols;
        _cells = new Cell[_rowsNumber, _colsNumber];
        _gridIndicesMap = new Dictionary<Item, List<Vector2Int>>();

        for (int row = 0; row < _rowsNumber; row++)
        {
            for (int col = 0; col < _colsNumber; col++)
            {
                _cells[row, col] = new Cell(cellAttributes);
            }
        }
    }

    public bool CanPlaceContent(Vector3 worldCoords, Item item)
    {
        Vector2Int startCoordinate = WorldToGrid(worldCoords);
        List<Vector2Int> cellIndices = GetOcupiedGridIndices(startCoordinate, item);

        if(cellIndices.Count == 0) 
        { 
            return false; 
        }

        bool canPlaceHere = true;
        foreach (Vector2Int cellIndex in cellIndices)
        {
            bool canPlaceThisCell = _cells[cellIndex.x, cellIndex.y].CanPlaceHere(item);
            canPlaceHere &= canPlaceThisCell;
        }
        return canPlaceHere;
    }


    public void PlaceContent(Vector3 worldCoords, Item item)
    {
        Vector2Int startCoordinate = WorldToGrid(worldCoords);
        List<Vector2Int> ocupiedIndices = GetOcupiedGridIndices(startCoordinate, item);

        foreach (var coordinate in ocupiedIndices)
        {
            _cells[coordinate.x, coordinate.y].PlaceContent(item);
        }
        _gridIndicesMap.Add(item, ocupiedIndices);
    }

    public Vector2Int GetItemOrigin(Item item)
    {
        return _gridIndicesMap[item][0];
    }

    public Item GetContentWithIndex(Vector2Int gridIdx)
    {
        return _cells[gridIdx.x, gridIdx.y].GetContent();
    }

    public Item GetContentWithCoords(Vector3 worldCoords)
    {
        Vector2Int gridIndexes = WorldToGrid(worldCoords);
        return GetContentWithIndex(gridIndexes);
    }

    public Vector3 GridToWorld(Vector2Int gridCoords)
    {
        float x = gridCoords.x * _cellWidth + _offset.x;
        float y = _offset.y;
        float z = gridCoords.y * _cellWidth + _offset.z;
        return new Vector3(x, y, z);
    }

    public bool CheckOnGrid(Vector3 worldCoordinates)
    {
        float xStart = _offset.x;
        float zStart = _offset.z;
        float xSize = _cellWidth * _rowsNumber;
        float zSize = _cellWidth * _colsNumber;
        float xEnd = xStart + xSize;
        float zEnd = zStart + zSize;

        return (xStart <= worldCoordinates.x) && (worldCoordinates.x <= xEnd) &&
            (zStart <= worldCoordinates.z) && (worldCoordinates.z <= zEnd);

    }

    public void ClearContentWithCoordinates(Vector3 coords)
    {
        var indexes = WorldToGrid(coords);
        ClearContentWithIndex(indexes.x, indexes.y);
    }

    public void ClearContentWithIndex(int row, int col)
    {
        _cells[row, col].RemoveContent();
    }

    public void ClearContentWithItem(Item item)
    {
        List<Vector2Int> ocupiedGridIndices = _gridIndicesMap[item];
        foreach (var itemIndices in ocupiedGridIndices)
        {
            ClearContentWithIndex(itemIndices.x, itemIndices.y);
        }
        _gridIndicesMap.Remove(item);

    }

    private Vector2Int WorldToGrid(Vector3 coords)
    {
        int row = Mathf.FloorToInt((coords.x - _offset.x) / _cellWidth);
        int col = Mathf.FloorToInt((coords.z - _offset.z) / _cellWidth);
        return new Vector2Int(row, col);
    }

    private List<Vector2Int> GetOcupiedGridIndices(Vector2Int startCoordinate, Item item)
    {
        List<Vector2Int> ocupiedCoortinates = new List<Vector2Int>();

        if (startCoordinate.x + item.Sizes.x > _cells.GetLength(0) ||
            startCoordinate.y + item.Sizes.y > _cells.GetLength(1))
        {
            return ocupiedCoortinates;
        }

        for (int row = 0; row < item.Sizes.x; row++)
        {
            for (int col = 0; col < item.Sizes.y; col++)
            {
                Vector2Int itemCoord = new Vector2Int(startCoordinate.x + row, startCoordinate.y + col);
                ocupiedCoortinates.Add(itemCoord);
            }
        }
        return ocupiedCoortinates;
    }

}
