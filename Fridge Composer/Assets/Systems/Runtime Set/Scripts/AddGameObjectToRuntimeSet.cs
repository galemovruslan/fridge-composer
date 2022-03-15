using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGameObjectToRuntimeSet : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSet _runtimeSet;

    protected void Add(GameObject thingToAdd)
    {
        _runtimeSet.AddObject(thingToAdd);
    }

    protected void Remove(GameObject thingToRemove)
    {
        _runtimeSet.RemoveObject(thingToRemove);
    }
}
