using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Descriptions/Grid", fileName = "New Grid Description")]
public class GridDescription : ScriptableObject
{
    public int Cols => _cols; 
    public int Width => _width; 
    public SurfaceDescription Description => _baseSurface; 
    public int Rows => _rows; 

    [SerializeField] private int _rows;
    [SerializeField] private int _cols;
    [SerializeField] private int _width;
    [SerializeField] private SurfaceDescription _baseSurface;
    [SerializeField] private List<ModifiedCell> _alternativeSurfaces;


    public SurfaceDescription GetDescription(Vector2Int lookIndex)
    {
        foreach (var surface in _alternativeSurfaces)
        {
            if(surface.Indices == lookIndex)
            {
                return surface.ModifiedSurface;
            }
        }
        return _baseSurface;
    }

    [System.Serializable]
    private struct ModifiedCell 
    {
        public Vector2Int Indices;
        public SurfaceDescription ModifiedSurface;

    }

}
