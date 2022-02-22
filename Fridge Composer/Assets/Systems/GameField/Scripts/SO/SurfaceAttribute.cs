
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Attributes/Surface", fileName ="New Surface Attribute")]
public class SurfaceAttribute : ScriptableObject
{
    [SerializeField] private List<ItemAttribute> _forbidenAttributes;

    public bool Contains(ItemAttribute itemAttribute)
    {
        return _forbidenAttributes.Contains(itemAttribute);
    }

}
