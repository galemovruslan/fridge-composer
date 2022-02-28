using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    [SerializeField] private GridDescription _gridDescription;

    private int _rows;
    private int _cols;
    private int _width;
    private Grid _grid;
    private BoxCollider _collider;
    private Dictionary<Item, PlaceableItem> _gameObjectMap = new Dictionary<Item, PlaceableItem>();

    void Awake()
    {
        _rows = _gridDescription.Rows;
        _cols = _gridDescription.Cols;
        _width = _gridDescription.Width;

        _grid = new Grid(_rows, _cols, _width, offset: transform.position, _gridDescription.Description);
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        FitCollider(_rows, _cols, _width, transform.position);
        DrawGridDebug();
    }

    public PlaceableItem RemoveFromGrid(Vector3 worldCoordinates)
    {
        Item itemOnCoordinates = _grid.GetContentWithCoords(worldCoordinates);

        if (itemOnCoordinates == null) 
        { 
            return null;
        }

        _grid.ClearContentWithItem(itemOnCoordinates);
        PlaceableItem removedObject = UnregisterObjectOnGrid(itemOnCoordinates);
        DrawGridDebug();
        return removedObject;
    }

    public bool TryPlaceOnGrid(Vector3 worldCoordinates, Item item, PlaceableItem placedObject)
    {
        if (!_grid.CanPlaceContent(worldCoordinates, item))
        {
            return false;
        }

        _grid.PlaceContentInCells(worldCoordinates, item);
        RegisterObjectOnGrid(item, placedObject);
        DrawGridDebug();
        return true;
    }

    public Vector3 SnapToGrid(Vector3 freeWorldCoordinates)
    {
        return _grid.SnapToGrid(freeWorldCoordinates);
    }

    private void RegisterObjectOnGrid(Item item, PlaceableItem spawnedGameObject)
    {
        _gameObjectMap.Add(item, spawnedGameObject);
    }

    private PlaceableItem UnregisterObjectOnGrid(Item item)
    {
        PlaceableItem unregisteredObject = _gameObjectMap[item];
        _gameObjectMap.Remove(item);
        return unregisteredObject;
    }

    private void DrawGridDebug()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                Vector2Int startCell = new Vector2Int(row, col);

                Vector2Int endCellHorizontal = new Vector2Int(row, col + 1);
                Vector3 startPoint = _grid.GridToWorld(startCell);
                Vector3 endPointHorizontal = _grid.GridToWorld(endCellHorizontal);
                Debug.DrawLine(startPoint, endPointHorizontal, Color.white, 999);

                Vector2Int endCellVertical = new Vector2Int(row + 1, col);
                Vector3 endPointVertical = _grid.GridToWorld(endCellVertical);
                Debug.DrawLine(startPoint, endPointVertical, Color.white, 999);

                Vector3 textOffset = new Vector3(_width / 2.0f, 0f, _width / 2.0f);
                Vector3 textPosition = transform.InverseTransformPoint( startPoint) + textOffset;

                var item = _grid.GetContentWithIndex(new Vector2Int(row, col));
                string cellText = item == null ? "null" : "1";

                Utils.CreateWorldText(cellText,
                    parent: transform,
                    localPosition: textPosition,
                    fontSize: 5,
                    color: Color.white);

            }
        }
        Debug.DrawLine(_grid.GridToWorld(new Vector2Int(0, _cols)), _grid.GridToWorld(new Vector2Int(_rows, _cols)), Color.white, 999);
        Debug.DrawLine(_grid.GridToWorld(new Vector2Int(_rows, 0)), _grid.GridToWorld(new Vector2Int(_rows, _cols)), Color.white, 999);

    }

    private void FitCollider(int rows, int cols, int cellWidth, Vector3 offset)
    {
        float xSize = cellWidth * rows;
        float zSize = cellWidth * cols;

        float gridCenterX = xSize / 2;
        float gridCenterZ = zSize / 2;
        _collider.center = new Vector3(gridCenterX, transform.position.y, gridCenterZ);
        _collider.size = new Vector3(xSize, _collider.size.y, zSize);
    }

}
