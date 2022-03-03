using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPopulator : MonoBehaviour
{
    [SerializeField] private GridInteractor _grid;
    [SerializeField] private List<PlaceableItem> _itemPrefabs;

    private void Start()
    {
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        foreach (var itemPrefab in _itemPrefabs)
        {
            PlaceableItem item = null;

            foreach (var spawnIndices in _grid.GetAllIndices())
            {
                if (item == null)
                {
                    item = Instantiate(itemPrefab);
                }
                // попробовать разместить с оригинальной ориентацией
                if (_grid.TryPlaceWithIndices(spawnIndices, item))
                {
                    item = null;
                    break;
                }

                // если не получилось, повернуть и попробовать разместить
                item.SwapOrientation();
                if (_grid.TryPlaceWithIndices(spawnIndices, item))
                {
                    item = null;
                    break;
                }

                // если не получилось, повернуть в оригинальную ориентацию и переидти в следующую ячейку
                item.SwapOrientation();

            }
            if (item != null)
            {
                Destroy(item.gameObject);
            }
        }
    }

}
