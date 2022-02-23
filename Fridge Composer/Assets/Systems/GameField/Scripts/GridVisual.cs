using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    [SerializeField] private PlaceableItem _placeable;
    [SerializeField] private GridDescription _gridDescription;

    private int _rows;
    private int _cols;
    private int _width;
    private Grid _grid;
    private Camera _camera;
    private BoxCollider _collider;
    private Dictionary<Item, List<Vector2Int>> _gridIndicesMap = new Dictionary<Item, List<Vector2Int>>();
    private Dictionary<Item, GameObject> _gameObjectMap = new Dictionary<Item, GameObject>();

    void Awake()
    {
        _rows = _gridDescription.Rows;
        _cols = _gridDescription.Cols;
        _width = _gridDescription.Width;

        _grid = new Grid(_rows, _cols, _width, offset: transform.position, _gridDescription.Description);
        _camera = Camera.main;
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        FitCollider(_rows, _cols, _width, transform.position);
        DrawGridDebug();
    }

    private void Update()
    {
        ClickOnGridHandler();
    }

    private void ClickOnGridHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandeleClick((Vector3 worldCoordinates) =>
            {
                PlaceOnGrid(worldCoordinates);
            });
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HandeleClick((Vector3 worldCoordinates) =>
            {
                RemoveFromGrid(worldCoordinates);
            });
        }
    }

    private void RemoveFromGrid(Vector3 worldCoordinates)
    {
        Item itemOnCoordinates = _grid.GetContentWithCoords(worldCoordinates);

        if (itemOnCoordinates == null) { return; }

        DestroyObjectOnGrid(itemOnCoordinates);
        List<Vector2Int> ocupiedGridIndices = _gridIndicesMap[itemOnCoordinates];
        foreach (var itemIndices in ocupiedGridIndices)
        {
            _grid.ClearContentWithIndex(itemIndices.x, itemIndices.y);
        }
        _gridIndicesMap.Remove(itemOnCoordinates);
        DrawGridDebug();
    }

    private void PlaceOnGrid(Vector3 worldCoordinates)
    {
        if (_grid.CanPlaceContent(worldCoordinates, _placeable.Item))
        {
            List<Vector2Int> ocupiedGridIndices = _grid.PlaceContent(worldCoordinates, _placeable.Item);
            _gridIndicesMap.Add(_placeable.Item, ocupiedGridIndices);
            SpawnObjectOnGrid(_placeable.Item, ocupiedGridIndices);
            DrawGridDebug();
        }
    }

    private void SpawnObjectOnGrid(Item item, List<Vector2Int> coordinates)
    {
        Vector3 worldCoords = _grid.GridToWorld(coordinates[0]);
        GameObject itemGameObject = Instantiate(item.Visuals, worldCoords, Quaternion.identity);
        _gameObjectMap.Add(item, itemGameObject);
    }

    private void DestroyObjectOnGrid(Item item)
    {
        Destroy(_gameObjectMap[item]);
        _gameObjectMap.Remove(item);
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
                Vector3 textPosition = startPoint + textOffset;

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
        float xStart = offset.x;
        float zStart = offset.z;
        float xSize = cellWidth * rows;
        float zSize = cellWidth * cols;

        float gridCenterX = xStart + xSize / 2;
        float gridCenterZ = zStart + zSize / 2;
        _collider.center = new Vector3(gridCenterX, transform.position.y, gridCenterZ);
        _collider.size = new Vector3(xSize, _collider.size.y, zSize);
    }

    private void HandeleClick(Action<Vector3> OnGridClickAction)
    {
        Ray screenRay = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out var hit))
        {
            Vector3 worldCoordinates = hit.point;
            if (!_grid.CheckOnGrid(worldCoordinates))
            {
                Debug.Log("Pointer in not on grid");
                return;
            }
            OnGridClickAction.Invoke(worldCoordinates);
        }
    }

}
