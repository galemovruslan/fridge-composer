using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("Если не понадобится функция замены экземпляра" +
    "Item на GameObject-е во время выполнения, то удалить")]
public class PlaceableItem : MonoBehaviour
{
    public Item Item { get => _item; }

    [SerializeField] private ItemDesciption _description;

    private Item _item;

    private void Awake()
    {
        _item = new Item(_description);
    }

    public void ChangeItem(ItemDesciption itemDesciption)
    {
        _description = itemDesciption;
        _item = new Item(_description);
    }
}
