using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickState : PlacerState
{
    public PickState(PlacerStateMachine stateMachine, PlaceableItem item) : base(stateMachine, item)
    {
    }

    public override void ClickHandle()
    {
        InteractWithGrid((grid, worldCoordinates) =>
        {
            PlaceableItem removedItem = grid.RemoveFromGrid(worldCoordinates);
            if (removedItem == null) { return; }

            _currentItem = removedItem;
            ChangeState();
        });
    }

    protected override void ChangeState()
    {
        var placeState = new PlaceState(_stateMachine, _currentItem);
        _stateMachine.ChangeState(placeState);
    }
}
