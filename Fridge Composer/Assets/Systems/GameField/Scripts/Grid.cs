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

    public Grid(int rows, int cols, int cellWidth, Vector3 offset)
    {
        _offset = offset;
        _cellWidth = cellWidth;
        _rowsNumber = rows;
        _colsNumber = cols;
        _cells = new Cell[_rowsNumber, _colsNumber];

        for (int row = 0; row < _rowsNumber; row++)
        {
            for (int col = 0; col < _colsNumber; col++)
            {
                _cells[row, col] = new Cell();
            }
        }
    }

    public bool CanPlaceContent(Vector3 worldCoords, Item content)
    {
        return true;
    }

    public void PlaceContent(Vector3 worldCoords, Item content)
    {
        var indexes = WorldToGrid(worldCoords);
        _cells[indexes.x, indexes.y].PlaceContent(content);
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

    public void ClearContent(Vector3 coords)
    {
        var indexes = WorldToGrid(coords);
        ClearCell(indexes.x, indexes.y);
    }

    private void ClearCell(int row, int col)
    {
        _cells[row, col].RemoveContent();
    }

    private Vector2Int WorldToGrid(Vector3 coords)
    {
        int row = Mathf.FloorToInt((coords.x - _offset.x) / _cellWidth);
        int col = Mathf.FloorToInt((coords.z - _offset.z) / _cellWidth);
        return new Vector2Int(row, col);
    }



}
