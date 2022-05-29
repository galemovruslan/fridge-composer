using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFocuser : MonoBehaviour
{
    // класс заменит CameraRig в RigSwitcher

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
        if (_gridInteractor == null)
        {
            return;
        }
        _gridInteractor.SetColliderEnable(true);
    }

    public void ResetFocus()
    {
        ResetPriority();
        if (_gridInteractor == null)
        {
            return;
        }
        _gridInteractor.SetColliderEnable(false);
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
