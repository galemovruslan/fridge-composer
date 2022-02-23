using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private SurfaceDescription _surfaceDescrption;

    private Item _content;

    public Cell(SurfaceDescription description)
    {
        _surfaceDescrption = description;
    }

    public void PlaceContent(Item cellContent)
    {
        _content = cellContent;
    }

    public Item GetContent()
    {
        return _content;
    }

    public void RemoveContent()
    {
        _content = null;
    }

    public bool CanPlaceHere(Item content)
    {
        return _content == null && !content.CheckForbidenAttributes(_surfaceDescrption);
    }
}
