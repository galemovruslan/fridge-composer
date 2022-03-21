using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridInteractor : MonoBehaviour
{
    [SerializeField] private ItemGameEvent _onPlaceItem;
    [SerializeField] private ItemGameEvent _onRemoveItem;

    [field: SerializeField]
    public bool InFridge { get; private set; }

    [SerializeField] private GridDescription _gridDescription;

    private int _rows;
    private int _cols;
    private int _width;
    private Grid _grid;
    private BoxCollider _collider;
    private Dictionary<Item, PlaceableItem> _gameObjectMap = new Dictionary<Item, PlaceableItem>();
    private Dictionary<Vector2Int, GameObject> _cellObjects = new Dictionary<Vector2Int, GameObject>();

    void Awake()
    {
        _rows = _gridDescription.Rows;
        _cols = _gridDescription.Cols;
        _width = _gridDescription.Width;

        Vector3 offset =  new Vector3(-_rows / 2.0f * _width, 0, -_cols / 2.0f * _width);

        _grid = new Grid(_rows, _cols, _width, offset, _gridDescription.GetDescription);
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        FitCollider(_rows, _cols, _width, transform.position);
        DrawCells();
    }

    public PlaceableItem RemoveFromGrid(Vector3 worldCoordinates)
    {
        Vector3 localCoordinates = ConvertToLocal(worldCoordinates);
        Item itemOnCoordinates = _grid.GetContent(localCoordinates);

        if (itemOnCoordinates == null)
        {
            return null;
        }

        _grid.ClearContentWithItem(itemOnCoordinates);
        PlaceableItem removedObject = UnregisterObjectOnGrid(itemOnCoordinates);
        
        if (InFridge)
        {
            _onRemoveItem.Raise(itemOnCoordinates);
        }
        return removedObject;
    }

    public bool TryPlaceOnGrid(Vector3 worldCoordinates, PlaceableItem placedObject)
    {
        Vector3 localCoordinates = ConvertToLocal(worldCoordinates);
        Vector2Int startIndices = _grid.WorldToGrid(localCoordinates);
        startIndices = _grid.ShiftFromEdge(startIndices, placedObject.Item);
        return TryPlaceOnGrid(startIndices, placedObject);
    }

    public bool TryPlaceOnGrid(Vector2Int indices, PlaceableItem placedObject)
    {
        if (!_grid.CanPlaceContent(indices, placedObject.Item))
        {
            return false;
        }

        placedObject.transform.position = ConvertToWorld(_grid.GridToWorld(indices));
        placedObject.transform.localRotation = transform.localRotation;
        _grid.PlaceContentInCells(indices, placedObject.Item);
        RegisterObjectOnGrid(placedObject.Item, placedObject);
        
        if (InFridge)
        {
            _onPlaceItem.Raise(placedObject.Item);
        }
        return true;
    }

    public Vector3 SnapToGrid(Vector3 freeWorldCoordinates)
    {
        Vector3 localCoordinates = ConvertToLocal(freeWorldCoordinates);
        return ConvertToWorld(_grid.SnapToGrid(localCoordinates));
    }

    public Vector3 SnapToGrid(Vector3 freeWorldCoordinates, Item item)
    {
        Vector3 localCoordinates = ConvertToLocal(freeWorldCoordinates);
        Vector3 snapedLocal =_grid.SnapToGrid(localCoordinates, item);
        return ConvertToWorld(snapedLocal);
    }

    public List<Vector2Int> GetGridIndices()
    {
        List<Vector2Int> indices = new List<Vector2Int>();
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                indices.Add(new Vector2Int(row, col));
            }
        }
        return indices;
    }

    public List<Vector2Int> PosiblePlaceIndices(Item item)
    {
        var availablePlaces = new List<Vector2Int>();
        foreach (Vector2Int posiblePlace in GetGridIndices())
        {
            bool isPlaceable = _grid.CanPlaceContent(posiblePlace, item);
            if (isPlaceable)
            {
                availablePlaces.Add(posiblePlace);
            }
        }
        return availablePlaces;
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

    private void DrawCells()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int cols = 0; cols < _cols; cols++)
            {
                SurfaceDescription cellDescription = _grid.GetCellDescription(row, cols);

                Vector2Int cellIndices = new Vector2Int(row, cols);
                Vector3 spawnCoordinates = _grid.GridToWorld(cellIndices);
                spawnCoordinates = ConvertToWorld(spawnCoordinates);
                GameObject cellVisual = Instantiate(cellDescription.Visual.gameObject, spawnCoordinates, transform.rotation, transform);
                _cellObjects.Add(cellIndices, cellVisual);
            }
        }
    }
    
    private void FitCollider(int rows, int cols, int cellWidth, Vector3 offset)
    {
        float xSize = cellWidth * rows;
        float zSize = cellWidth * cols;

        _collider.center = new Vector3(0, 0, 0);
        _collider.size = new Vector3(xSize, _collider.size.y, zSize);
    }

    private Vector3 ConvertToLocal(Vector3 worldCoordinates) => transform.InverseTransformPoint(worldCoordinates);

    private Vector3 ConvertToWorld(Vector3 localCordinates) => transform.TransformPoint(localCordinates);
}
