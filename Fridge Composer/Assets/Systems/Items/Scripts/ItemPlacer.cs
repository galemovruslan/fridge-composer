using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{

    [SerializeField] private ItemDesciption _currentItem;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        HandeleClick();
    }

    private void HandeleClick()
    {
        InputHandle();
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

    private void InputHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var itemToPlace = new Item(_currentItem);
            InteractWithGrid((grid, worldCoordinates) => grid.PlaceOnGrid(worldCoordinates, itemToPlace));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            InteractWithGrid((grid, worldCoordinates) => grid.RemoveFromGrid(worldCoordinates));

        }
    }
}
