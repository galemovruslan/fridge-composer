using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public SurfaceDescription SurfaceDescrption => _surfaceDescrption;
    public bool HasSingleItem => _content.Count <= 1;

    private SurfaceDescription _surfaceDescrption;
    private SurfaceDescription _originalSurfaceDescrption;
    private Stack<Item> _content = new Stack<Item>(2);

    public Cell(SurfaceDescription description)
    {
        _surfaceDescrption = description;
        _originalSurfaceDescrption = description;
    }

    public void PlaceContent(Item cellContent)
    {
        _content.Push(cellContent);
        if (_content.Peek().HasOverride)
        {
            _surfaceDescrption = _content.Peek().OverrideDescription(_surfaceDescrption);
        }
    }

    public Item GetContent()
    {
        return _content.Count == 0 ? null : _content.Peek();
    }

    public void RemoveContent()
    {
        if (_content.Peek().HasOverride)
        {
            _surfaceDescrption = _originalSurfaceDescrption;
        }
        _content.Pop();
    }

    public bool CanPlaceHere(Item content)
    {
        return (_content.Count == 0 || _content.Peek().isStackable && !content.isStackable) 
            && !content.CheckForbidenAttributes(_surfaceDescrption);
    }
}
