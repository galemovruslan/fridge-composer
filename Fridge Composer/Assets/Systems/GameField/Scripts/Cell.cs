using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private int _row;
    private int _col;
    private Item _content;

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

    private bool CanPlaceHere(Item content)
    {
        return true;
    }
}
