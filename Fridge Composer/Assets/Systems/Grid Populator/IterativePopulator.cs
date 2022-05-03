using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IterativePopulator : IGridPopulator
{
    private GridInteractor _grid;
    private ItemComposer _composer;
    private ItemSpawner _spawner;

    public IterativePopulator(GridInteractor grid, ItemSpawner spawner)
    {
        _grid = grid;
        _composer = new ItemComposer(_grid);
        _spawner = spawner;
    }

    public List<PlaceableItem> PopulateGrid(List<ItemDesciption> itemsToPlace)
    {
        List<Item> itemsModels = itemsToPlace.Select(description => new Item(description)).ToList();

        List<ItemComposer.PlacementDescription> itemsToSpawn = _composer.Compose(itemsModels);
        List<PlaceableItem> placedItems = SpawnItems(itemsToSpawn);

        return placedItems;
    }

    private List<PlaceableItem> SpawnItems(List<ItemComposer.PlacementDescription> spawnList)
    {
        List<PlaceableItem> placedItems = new List<PlaceableItem>(spawnList.Count);

        foreach (var spawnDescription in spawnList)
        {
            PlaceableItem spawnedItem = _spawner.Spawn(spawnDescription.ComposedItem.Description);

            if (!spawnDescription.IsDefaulfOrient)
            {
                spawnedItem.SwapOrientation();
            }

            _grid.TryPlaceOnGrid(spawnDescription.Location, spawnedItem);
            placedItems.Add(spawnedItem);
        }

        return placedItems;
    }


}
