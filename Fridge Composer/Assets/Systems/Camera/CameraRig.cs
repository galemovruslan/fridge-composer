using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraRig : MonoBehaviour
{
    public event Action<CameraRig> OnSelect;

    [SerializeField] private CinemachineVirtualCamera _rigCamera;
    [SerializeField] private GridSelectionButton _buttonPrefab;

    private GridSelectionButton _selectionButton;


    private void OnEnable()
    {
        _selectionButton = Instantiate(_buttonPrefab);
        _selectionButton.Initialize(gameObject.name);
        _selectionButton.AddListener(SelectionHandle);

    }

    public void SelectionHandle()
    {
        OnSelect?.Invoke(this);
    }

    public void SetPriority()
    {
        _rigCamera.Priority = 1;
    }

    public void ResetPriority()
    {
        _rigCamera.Priority = 0;
    }
}