using System;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public bool isStackable => _description.IsStackable;
    public bool HasOverride => _description.HasOverride;
    public bool IsSymetrical => _sizes.x == _sizes.y;
    public Vector2Int Sizes => _sizes;
    public GameObject Visuals => _description.Visuals;

    private ItemDesciption _description;
    private Vector2Int _sizes;

    public Item(ItemDesciption description)
    {
        _description = description;
        _sizes = new Vector2Int(_description.RowNum, _description.ColNum);
    }

    public void SwapOrientation()
    {
        if(_sizes.x == _sizes.y)
        {
            return;
        }
        int temp = _sizes.x;
        _sizes.x = _sizes.y;
        _sizes.y = temp;
    }

    public SurfaceDescription OverrideDescription(SurfaceDescription surfaceDescrption)
    {
        SurfaceDescription newSurface = SurfaceDescription.Instantiate(surfaceDescrption);
        foreach (SurfaceOverrideDefinition overrideDescription in _description.OverrideDescription)
        {
            if (newSurface.ContainsAttribute(overrideDescription.From))
            {
                newSurface.ReplaceAttribute(overrideDescription.From, overrideDescription.To);
            }

        }
        return newSurface;
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