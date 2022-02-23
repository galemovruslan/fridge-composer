using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Vector2Int Sizes => new Vector2Int(_description.RowNum, _description.ColNum);
    public GameObject Visuals => _description.Visuals;


    private ItemDesciption _description;
    
    public Item(ItemDesciption description)
    {
        _description = description;
    }

    public bool CheckForbidenAttributes(SurfaceDescription surfaceDescription)
    {
        bool foundForbiden = false;
        foreach (var surfaceAttribute in surfaceDescription.Attributes)
        {
            foreach (var itemAttribute in _description.Attributes)
            {
                if (surfaceAttribute.Contains(itemAttribute))
                {
                    foundForbiden = true;
                    break;
                }

            }
        }
        return foundForbiden;
    }

}