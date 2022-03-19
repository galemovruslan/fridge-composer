using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GridSelectionButton : MonoBehaviour
{

    private Text _text;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<Text>();
        transform.parent = FindObjectOfType<NavigationButtonsSpawner>().transform;
    }

    public void Initialize(string name)
    {
        gameObject.name = name;
        _text.text = name;
    }

    public void AddListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

}
