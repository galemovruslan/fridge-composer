using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRig))]
public class SelectionButtonSpawner : MonoBehaviour
{/*
    [SerializeField] private GridSelectionButton _buttonPrefab;

    private CameraRig _rig;
    private GridSelectionButton _selectionButton;

    private void OnEnable()
    {
        _rig = GetComponent<CameraRig>();
        _selectionButton = Instantiate(_buttonPrefab);
        _selectionButton.Initialize(_rig.gameObject.name);
        _selectionButton.AddListener(_rig.SelectionHandle);

    }*/
}
