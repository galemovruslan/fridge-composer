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
        // ƒл€ каждой сетки
        foreach (var gridObject in _gridSet.GetObjects())
        {
            // найти попул€тор
            if (gridObject.TryGetComponent<GridPopulator>(out var populator))
            {
                if (!populator.InFridge)
                {
                    continue;
                }
                // если сетка в холодильнике то подбираем список возможных предметов дл€ установки

                // 
            }
        }
    }

    /*
     * 1) определить какие предметы и в каком количестве использовать дл€ заполнени€
     *  холодильника на указанный процент
     * 2) расставить предметы на случайные поверхности
     */



}
