using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFocuser : MonoBehaviour
{
    // ����� ������� CameraRig � RigSwitcher

    public event Action<GridFocuser> OnSelect;
    public bool InFridge => _gridInteractor.InFridge;


    [SerializeField] private CameraRig _cameraRig;
    [SerializeField] private GridInteractor _gridInteractor;

    private void OnEnable()
    {
        _cameraRig.OnSelect += OnSelectionButtonPress;
    }

    private void OnDisable()
    {
        _cameraRig.OnSelect -= OnSelectionButtonPress;
    }

    public void OnSelectionButtonPress(CameraRig rig)
    {
        OnSelect?.Invoke(this);
    }

    public void SetFocus()
    {
        SetPriority();
    }

    public void ResetFocus()
    {
        ResetPriority();
    }

    private void ResetPriority()
    {
        _cameraRig.ResetPriority();
    }

    private void SetPriority()
    {
        _cameraRig.SetPriority();
    }


}
