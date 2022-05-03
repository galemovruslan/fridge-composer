using System.Collections.Generic;

public interface IGridPopulator
{
    List<PlaceableItem> PopulateGrid(List<ItemDesciption> itemsToPlace, float fillRatio);


}
