using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RigSwitcher : MonoBehaviour
{
    [SerializeField] private RuntimeRepository _rigRepository;

    private List<CameraRig> _sceneRigs = new List<CameraRig>();

    private void Start()
    {
        _sceneRigs.AddRange(_rigRepository.GetObjects().Select(obj => obj.GetComponent<CameraRig>()));
        _sceneRigs.ForEach((rig) => rig.OnSelect += OnSwitch);
    }

    private void OnDisable()
    {
        _sceneRigs.ForEach((rig) => rig.OnSelect -= OnSwitch);

    }

    private void OnSwitch(CameraRig activeRig)
    {
        _sceneRigs.ForEach((rig) => rig.ResetPriority());
        activeRig.SetPriority();
    }

}
