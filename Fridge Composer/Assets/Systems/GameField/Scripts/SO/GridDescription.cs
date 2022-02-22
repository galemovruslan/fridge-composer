using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Descriptions/Grid", fileName = "New Grid Description")]
public class GridDescription : ScriptableObject
{
    public int Cols => _cols; 
    public int Width => _width; 
    public SurfaceDescription Description => _description; 
    public int Rows => _rows; 

    [SerializeField] private int _rows;
    [SerializeField] private int _cols;
    [SerializeField] private int _width;
    [SerializeField] private SurfaceDescription _description;
}
