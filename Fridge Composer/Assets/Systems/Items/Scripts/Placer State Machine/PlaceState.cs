using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceState : PlacerState
{

    public PlaceState(PlacerStateMachine stateMachine, PlaceableItem item) : base(stateMachine, item)
    {
    }
    
    public override void Tick()
    {
        if (_currentItem == null) { return; }

        Item itemToPlace = _currentItem.Item;

        InteractWithGrid((grid, worldCoordinates) =>
        {
            if (!grid.TryPlaceOnGrid(worldCoordinates, itemToPlace, _currentItem)) { return; }

            _currentItem = null;
            ChangeState();
        });
    }

    protected override void ChangeState()
    {
        var placeState = new PickState(_stateMachine, _currentItem);
        _stateMachine.ChangeState(placeState);
    }
}
