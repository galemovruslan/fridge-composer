using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Descriptions/Item", fileName = "New Item Description")]
public class ItemDesciption : ScriptableObject
{
    public bool HasOverride => !(_overrideSurfaceAttributes.Count == 0);
    public IEnumerable<ItemAttribute> Attributes => _attributes;
    public int RowNum => _rowNum;
    public int ColNum => _colNum;
    public GameObject Visuals => _visuals;

    public IEnumerable<SurfaceOverrideDefinition> OverrideDescription => _overrideSurfaceAttributes;

    public bool IsStackable => _isStackable; 

    [SerializeField] private List<ItemAttribute> _attributes;

    [SerializeField] private List<SurfaceOverrideDefinition> _overrideSurfaceAttributes;

    [SerializeField] private bool _isStackable;

    [Min(1)]
    [SerializeField] private int _rowNum = 1;

    [Min(1)]
    [SerializeField] private int _colNum = 1;

    [SerializeField] private GameObject _visuals;

}