using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Descriptions/Surface", fileName = "New Surface Description")]
public class SurfaceDescription : ScriptableObject
{
    public IEnumerable<SurfaceAttribute> Attributes => _attributes;

    public CellVisual Visual => _visual;

    [SerializeField] private List<SurfaceAttribute> _attributes;
    [SerializeField] private CellVisual _visual;

    public bool ContainsAttribute(SurfaceAttribute attribute)
    {
        return _attributes.Contains(attribute);
    }

    public void ReplaceAttribute(SurfaceAttribute from, SurfaceAttribute to)
    {
        _attributes.Remove(from);
        _attributes.Add(to);
    }

}
