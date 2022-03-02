using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Descriptions/Surface", fileName = "New Surface Description")]
public class SurfaceDescription : ScriptableObject
{
    public List<SurfaceAttribute> Attributes => _attributes;

    public CellVisual Visual => _visual;

    [SerializeField] private List<SurfaceAttribute> _attributes;
    [SerializeField] private CellVisual _visual;
}
