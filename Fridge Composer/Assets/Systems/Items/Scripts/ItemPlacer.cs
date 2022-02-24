using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{

    [SerializeField] private PlaceableItem _currentItem;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        InputHandle();
        MoveItem();
    }

    private void InputHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var itemToPlace = _currentItem.Item;
            InteractWithGrid((grid, worldCoordinates) =>
            {
                if (grid.TryPlaceOnGrid(worldCoordinates, itemToPlace, _currentItem.gameObject))
                {
                    _currentItem.IsPlaced(true);
                }
            });
        }
        else if (Input.GetMouseButtonDown(1))
        {
            InteractWithGrid((grid, worldCoordinates) => grid.RemoveFromGrid(worldCoordinates));

        }
    }

    private void MoveItem()
    {
        InteractWithGrid((grid, worldCoordinates) =>
       {
           Vector3 snapedCoordinates = grid.SnapToGrid(worldCoordinates);
           _currentItem.transform.position = snapedCoordinates;
       });
    }

    private void InteractWithGrid(Action<GridVisual, Vector3> interaction)
    {
        Ray screenRay = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out var hit))
        {
            if (!hit.transform.TryGetComponent<GridVisual>(out GridVisual grid)) { return; }

            Vector3 worldCoordinates = hit.point;
            interaction.Invoke(grid, worldCoordinates);
        }
    }
}


