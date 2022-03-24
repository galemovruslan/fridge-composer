using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public SurfaceDescription SurfaceDescrption => _surfaceDescrption;

    private SurfaceDescription _surfaceDescrption;
    private SurfaceDescription _originalSurfaceDescrption;
    private Item _content;

    public Cell(SurfaceDescription description)
    {
        _surfaceDescrption = description;
        _originalSurfaceDescrption = description;
    }

    public void PlaceContent(Item cellContent)
    {
        _content = cellContent;
        if (_content.HasOverride)
        {
            _surfaceDescrption = _content.OverrideDescription(_surfaceDescrption);
        }
    }

    public Item GetContent()
    {
        return _content;
    }

    public void RemoveContent()
    {
        if (_content.HasOverride)
        {
            _surfaceDescrption = _originalSurfaceDescrption;
        }
        _content = null;
    }

    public bool CanPlaceHere(Item content)
    {
        return _content == null && !content.CheckForbidenAttributes(_surfaceDescrption);
    }
}
