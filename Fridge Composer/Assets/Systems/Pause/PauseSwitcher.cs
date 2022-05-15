using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSwitcher : MonoBehaviour
{
    [SerializeField] private BoolGameEvent _pauseSwitch;
    [SerializeField] private Menu _pauseMenu;
    [SerializeField] private Button _pauseButton;

    private bool _pauseState;

    private void Awake()
    {
        _pauseMenu.Hide();
    }

    private void OnEnable() => _pauseButton.onClick.AddListener(SetPause);

    private void OnDisable() => _pauseButton.onClick.RemoveAllListeners();


    public void SetPause()
    {
        _pauseState = true;
        _pauseSwitch.Raise(_pauseState);
        _pauseMenu.Show(this);
    }

    public void ResetPause()
    {
        _pauseState = false;
        _pauseSwitch.Raise(_pauseState);
        _pauseMenu.Hide();
    }
}
