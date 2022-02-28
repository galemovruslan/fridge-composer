using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{

    [SerializeField] private PlaceableItem _currentItemPrefab;

    private PlacerStateMachine _stateMachine;

    private PlaceableItem _currentItem;
    private Camera _camera;


    private void Awake()
    {
        _camera = Camera.main;
        _currentItem = Instantiate(_currentItemPrefab);

        _stateMachine = new PlacerStateMachine();
        var startState = new PlaceState(_stateMachine, _currentItem);
        _stateMachine.ChangeState(startState);
    }

    private void Update()
    {
        
        InputHandle();
        _stateMachine.Update();
        //MoveItem();
    }
    
    private void InputHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _stateMachine.Tick();
            //PlaceItem();
        }
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    PickItem();
        //}
    }
    /*
    private void PickItem()
    {
        InteractWithGrid((grid, worldCoordinates) =>
        {
            PlaceableItem removedItem = grid.RemoveFromGrid(worldCoordinates);

            if (removedItem == null) { return; }

            _currentItem = removedItem;
        });
    }

    private void PlaceItem()
    {
        if (_currentItem == null) { return; }

        Item itemToPlace = _currentItem.Item;

        InteractWithGrid((grid, worldCoordinates) =>
        {
            if (!grid.TryPlaceOnGrid(worldCoordinates, itemToPlace, _currentItem)) { return; }

            _currentItem.SetPlaced(true);
            _currentItem = null;
        });
    }

    private void MoveItem()
    {
        if (_currentItem == null) { return; };

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
    */
}


