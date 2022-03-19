using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItem : MonoBehaviour
{
    public Item Item => _item;

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
        _visuals.transform.Rotate(new Vector3(0, newOrientation, 0));

        float newTranslation = _isHorizontal ? 1f : -1f;
        _visuals.transform.Translate(new Vector3(_description.ColNum*newTranslation, 0, 0), Space.World);

        _isHorizontal = !_isHorizontal;
    }

    public void MoveTo(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

}
