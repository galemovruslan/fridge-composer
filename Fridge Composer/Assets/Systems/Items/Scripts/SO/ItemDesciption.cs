using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Descriptions/Item", fileName = "New Item Description")]
public class ItemDesciption : ScriptableObject
{
    public List<ItemAttribute> Attributes => _attributes;
    public int RowNum => _rowNum; 
    public int ColNum => _colNum;
    public GameObject Visuals => _visuals; 


    [SerializeField] private List<ItemAttribute> _attributes;
    
    [Min(1)]
    [SerializeField] private int _rowNum = 1;
    
    [Min(1)]
    [SerializeField] private int _colNum = 1;

    [SerializeField] private GameObject _visuals;
}