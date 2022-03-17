using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PlacerState
{
    protected PlaceableItem _currentItem;
    protected PlacerStateMachine _stateMachine;
    
    private Camera _camera;

    public abstract void ClickHandle();
    protected abstract void ChangeState();

    public PlacerState(PlacerStateMachine stateMachine, PlaceableItem item)
    {
        _currentItem = item;
        _stateMachine = stateMachine;
        _camera = Camera.main;
    }

    public void RotateHandle()
    {
        if (_currentItem == null)
        {
            return;
        }
        _currentItem.SwapOrientation();
    }


    public void MoveItem()
    {
        if (_currentItem == null) { return; };

        InteractWithGrid((grid, worldCoordinates) =>
        {
            Vector3 snapedCoordinates = grid.SnapToGrid(worldCoordinates);
            _currentItem.transform.position = snapedCoordinates;
        });
    }

    protected void InteractWithGrid(Action<GridInteractor, Vector3> interaction)
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        Ray screenRay = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out var hit))
        {
            if (!hit.transform.TryGetComponent<GridInteractor>(out GridInteractor grid)) { return; }

            Vector3 worldCoordinates = hit.point;
            interaction.Invoke(grid, worldCoordinates);
        }
    }

}
