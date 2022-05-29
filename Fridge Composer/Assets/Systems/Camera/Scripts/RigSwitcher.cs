using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RigSwitcher : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSet _rigRepository;

    private List<GridFocuser> _sceneFocusers = new List<GridFocuser>();

    private void Start()
    {
        _sceneFocusers.AddRange(_rigRepository.GetObjects().Select(obj => obj.GetComponent<GridFocuser>()));
        _sceneFocusers.ForEach((rig) => rig.OnSelect += OnSwitch);
    }

    private void OnDisable()
    {
        _sceneFocusers.ForEach((rig) => rig.OnSelect -= OnSwitch);
    }

    private void OnSwitch(GridFocuser activeFocuser)
    {
        _sceneFocusers.ForEach((rig) => rig.ResetFocus());
        activeFocuser.SetFocus();
    }

}
