using UnityEngine;

public struct GridRect
{
    public int _row;
    public int _col;
    public int _width;
    public int _height;

    public GridRect(int row, int col, int width, int height)
    {
        _row = row;
        _col = col;
        _width = width;
        _height = height;
    }
}