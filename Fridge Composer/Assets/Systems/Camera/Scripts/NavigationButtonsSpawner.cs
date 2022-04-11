using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationButtonsSpawner : MonoBehaviour
{
    [SerializeField] private GridSelectionButton _buttonPrefab;
    [SerializeField] private GameObjectRuntimeSet _rigs;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        GridSelectionButton selectionButton;
        List<OrderedButtons> spawnedButtons = new List<OrderedButtons>();
        foreach (var rigGO in _rigs.GetObjects())
        {
            CameraRig rig = rigGO.GetComponent<CameraRig>();
            selectionButton = Instantiate(_buttonPrefab);
            selectionButton.Initialize(rig.gameObject.name);
            selectionButton.AddListener(rig.SelectionHandle);
            spawnedButtons.Add(new OrderedButtons { button = selectionButton, order = rig.Order });
        }
        spawnedButtons.Sort((x, y) => x.order.CompareTo(y.order));
        spawnedButtons.ForEach(btn => btn.button.AssignParent(transform));
    }

    struct OrderedButtons
    {
        public GridSelectionButton button;
        public int order;
    }
}
