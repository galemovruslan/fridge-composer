using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<ItemAttribute> _attributes;

    private void Awake()
    {
        var guids = UnityEditor.AssetDatabase.FindAssets("t:"+typeof(ItemAttribute).Name);


        

        foreach (var id in guids)
        {
            var assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(id);
            var assetObj = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemAttribute>(assetPath);
            _attributes.Add(assetObj);
            Debug.Log(assetObj.name);
        }
    }
}
