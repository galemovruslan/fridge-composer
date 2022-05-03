using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPopulator : MonoBehaviour
{
    private const string _spawnParentName = "Items";

    [SerializeField] private float _fillRatio = 0.95f;
    [SerializeField] private float _leaveRatio = 0.5f;
    [SerializeField] private GameObjectRuntimeSet _gridSet;
    [SerializeField] private List<ItemDesciption> _itemPool;

    private Transform _spawnParent;
    private List<GridPopulator> _fridgePopulators = new List<GridPopulator>();
    private List<PlaceableItem> _spawnedItems = new List<PlaceableItem>();
    private List<GridInteractor> _outerGrids = new List<GridInteractor>();

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
        FillOutSurfaces();
    }

    private void FillOutSurfaces()
    {
        ItemReplacer replacer = new ItemReplacer();
        replacer.Replace(_outerGrids, _spawnedItems, 0.25f);
    }

    private void FillFridge()
    {
        foreach (var populator in _fridgePopulators)
        {
            _spawnedItems.AddRange(populator.PopulateGrid(_itemPool, _fillRatio));
        }
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
                }
                else
                {
                    _outerGrids.Add(gridObject.GetComponent<GridInteractor>());
                }
            }
        }

        /*
         * 1) определить какие предметы и в каком количестве использовать для заполнения
         *  холодильника на указанный процент
         * 2) расставить предметы на случайные поверхности
         */



    }
}