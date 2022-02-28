using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerStateMachine
{
    private PlacerState _currentState;


    public void ChangeState(PlacerState newState)
    {
        _currentState = newState;
    }

    public void Update()
    {
        _currentState.MoveItem();
    }

    public void Tick()
    {
        _currentState.Tick();
    }

}
