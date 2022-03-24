using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceOverrideDefinition : ScriptableObject
{
    [SerializeField] private SurfaceAttribute _from;

    [SerializeField] private SurfaceAttribute _to;

    public SurfaceAttribute From => _from;
    public SurfaceAttribute To => _to;




}
