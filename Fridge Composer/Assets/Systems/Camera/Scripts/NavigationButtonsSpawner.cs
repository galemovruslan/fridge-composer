using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationButtonsSpawner : MonoBehaviour
{
    [SerializeField] private GridSelectionButton _buttonPrefab;
    [SerializeField] private GameObjectRuntimeSet _rigs;

    private GridSelectionButton _selectionButton;

    private void Start()
    {
        Spawn();

    }

    private void Spawn()
    {
        foreach (var rigGO in _rigs.GetObjects())
        {
            CameraRig rig = rigGO.GetComponent<CameraRig>();
            _selectionButton = Instantiate(_buttonPrefab);
            _selectionButton.Initialize(rig.gameObject.name);
            _selectionButton.AddListener(rig.SelectionHandle);
        }
    }
}
