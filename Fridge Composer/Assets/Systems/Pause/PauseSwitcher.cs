using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSwitcher : MonoBehaviour
{
    [SerializeField] private BoolGameEvent _pauseSwitch;
    [SerializeField] private BoolGameEvent _onGameOver;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private Button _pauseButton;

    private bool _pauseState;

    private void Awake()
    {
        _pauseMenu.Hide();
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _onGameOver.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveAllListeners();
        _onGameOver.RemoveListener(OnGameOver);
    }

    public void ResetPause()
    {
        _pauseState = false;
        _pauseSwitch.Raise(_pauseState);
        _pauseMenu.Hide();
    }

    private void SetPause(PauseReason reason)
    {
        _pauseState = true;
        _pauseSwitch.Raise(_pauseState);

        switch (reason)
        {
            case PauseReason.button:
                _pauseMenu.ShowPause(this);
                break;

            case PauseReason.win:
                _pauseMenu.ShowWin(this);
                break;

            case PauseReason.lose:
                _pauseMenu.ShowLose(this);
                break;
        }


    }

    private void OnPauseButtonClick()
    {
        SetPause(PauseReason.button);
    }

    private void OnGameOver(bool isWin)
    {
        PauseReason reason = isWin ? PauseReason.win : PauseReason.lose;
        SetPause(reason);
    }

    private enum PauseReason
    {
        button,
        win,
        lose
    }


}
