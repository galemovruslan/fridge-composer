using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItem : MonoBehaviour
{
    public Item Item => _item;
    public ItemDesciption Description => _description; 

    [SerializeField] private ItemDesciption _description;

    private Item _item;
    private GameObject _visuals;
    private bool _isHorizontal = true;
    
    private void Awake()
    {
        _item = new Item(_description);
        _visuals = Instantiate(_description.Visuals, transform);
    }

    public void SwapOrientation()
    {
        if(_item.IsSymetrical)
        {
            return;
        }

        _item.SwapOrientation();

        float newOrientation = _isHorizontal ? -90f : 90f;
        _visuals.transform.Rotate(new Vector3(0, newOrientation, 0), Space.World);

        Vector3 newTranslation = _isHorizontal ? new Vector3(0, 0, -_item.Sizes.x) : new Vector3(-_item.Sizes.y, 0,0);
        _visuals.transform.Translate(newTranslation, Space.Self);

        _isHorizontal = !_isHorizontal;
    }

    public void MoveTo(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void AllignTo(Quaternion rotaion)
    {
        transform.localRotation = rotaion;
    }

}
