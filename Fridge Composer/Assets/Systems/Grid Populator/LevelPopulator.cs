using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPopulator : MonoBehaviour
{
    private const string _spawnParentName = "Items";

    [SerializeField] private float _fillRatio = 1f;
    [SerializeField] private GameObjectRuntimeSet _gridSet;

    private AvailablePlaceFinder _placeFinder;
    private Transform _spawnParent;
    private List<GridPopulator> _fridgePopulators = new List<GridPopulator>();
    private List<GridPopulator> _outerPopulators = new List<GridPopulator>();



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
        //_placeFinder = new AvailablePlaceFinder();
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
        throw new NotImplementedException();
    }

    private void FillFridge()
    {
        foreach (var populator in _fridgePopulators)
        {
           
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
                    _outerPopulators.Add(populator);
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