using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{

    [SerializeField] private PlaceableItem _currentItemPrefab;

    private PlacerStateMachine _stateMachine;
    private PlaceableItem _currentItem;

    private void Awake()
    {
        _currentItem = Instantiate(_currentItemPrefab);

        _stateMachine = new PlacerStateMachine();
        var startState = new PlaceState(_stateMachine, _currentItem);
        _stateMachine.ChangeState(startState);
    }

    private void Update()
    {
        
        InputHandle();
        _stateMachine.Update();
    }
    
    private void InputHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _stateMachine.ClickHandle();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _stateMachine.RotateHandle();
        }

    }
  
}


