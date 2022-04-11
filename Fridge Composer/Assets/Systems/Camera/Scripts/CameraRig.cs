using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraRig : MonoBehaviour
{
    public event Action<CameraRig> OnSelect;

    [field:SerializeField]
    public int Order { get; private set; }

    [SerializeField] private CinemachineVirtualCamera _rigCamera;

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