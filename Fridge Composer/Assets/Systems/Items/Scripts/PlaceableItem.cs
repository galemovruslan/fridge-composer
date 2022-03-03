using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItem : MonoBehaviour
{
    public Item Item => _item;

    [SerializeField] private ItemDesciption _description;

    private Item _item;
    private GameObject _visuals;
    bool _originalOrientation = true;
    private void Awake()
    {
        _item = new Item(_description);
        _visuals = Instantiate(_description.Visuals, transform);
    }

    public void SwapOrientation()
    {
        if(_item.Sizes.x == _item.Sizes.y)
        {
            return;
        }

        _item.SwapOrientation();

        float newOrientation = _originalOrientation ? -90f : 90f;
        _visuals.transform.Rotate(new Vector3(0, newOrientation, 0));

        float newTranslation = _originalOrientation ? 1f : -1f;
        _visuals.transform.Translate(new Vector3(newTranslation, 0, 0), Space.World);

        _originalOrientation = !_originalOrientation;
    }
}
