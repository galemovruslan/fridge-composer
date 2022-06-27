using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPopulator : MonoBehaviour
{
    private const string _spawnParentName = "Items";

    [SerializeField] private float _fillRatio = 0.95f;
    [SerializeField] private float _replaceRatio = 0.5f;
    [SerializeField] private GameObjectRuntimeSet _gridSet;
    [SerializeField] private List<ItemDesciption> _itemPool;
    [SerializeField] private BoolGameEvent _onLevelPopulated;
    [SerializeField] private ItemGameEvent _onCreateItem;

    private Transform _spawnParent;
    private List<GridPopulator> _fridgePopulators = new List<GridPopulator>();
    private List<PlaceableItem> _spawnedItems = new List<PlaceableItem>();
    private List<GridInteractor> _outerGrids = new List<GridInteractor>();
    private List<GridInteractor> _innerGrids = new List<GridInteractor>();
    
    private void Awake()
    {
        var parentGameObject = GameObject.Find(_spawnParentName);
        if (parentGameObject == null)
        {
            parentGameObject = new GameObject(_spawnParentName);
        }
        _spawnParent = parentGameObject.transform;
    }

    private void Start()
    {
        PopulateLevel();
    }

    private void PopulateLevel()
    {
        UnpackPopulators();
        FillFridge();
        MoveFromFridge();

        _onLevelPopulated.Raise(true);
    }

    private void MoveFromFridge()
    {
        ItemReplacer replacer = new ItemReplacer();
        replacer.Replace(_innerGrids, _outerGrids, _spawnedItems, _replaceRatio);
    }

    private void FillFridge()
    {
        foreach (var populator in _fridgePopulators)
        {
            _spawnedItems.AddRange(populator.PopulateGrid(_itemPool, _fillRatio));
        }
        _spawnedItems.ForEach(item => _onCreateItem.Raise(item.Item));
    }

    private void UnpackPopulators()
    {
        // Для каждой сетки
        foreach (var gridObject in _gridSet.GetObjects())
        {
            // найти популятор
            if (gridObject.TryGetComponent<GridPopulator>(out var populator))
            {
                if (populator.InFridge)
                {
                    _fridgePopulators.Add(populator);
                    _innerGrids.Add(gridObject.GetComponent<GridInteractor>());
                }
                else
                {
                    _outerGrids.Add(gridObject.GetComponent<GridInteractor>());
                }
            }
        }
    }

}