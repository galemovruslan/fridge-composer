using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConditionWatcher : MonoBehaviour, IPauseable
{

    [SerializeField] private ConditionSet _winConditions;
    [SerializeField] private ConditionSet _loseConditions;
    [SerializeField] private BoolGameEvent _onPause;
    [SerializeField] private BoolGameEvent _onLevelPopulated;
    [SerializeField] private BoolGameEvent _onGameOver;

    private void OnEnable()
    {
        _winConditions.AddListener(HandleWin);
        _loseConditions.AddListener(HanldeLose);
        _onLevelPopulated.AddListener(HandleLevelPopulated);
        _onPause.AddListener(HandePause);
        _winConditions.ResetConditions();
        _loseConditions.ResetConditions();
    }

    private void OnDisable()
    {
        _winConditions.RemoveListener(HandleWin);
        _loseConditions.RemoveListener(HanldeLose);
        _onLevelPopulated.RemoveListener(HandleLevelPopulated);
        _onPause.RemoveListener(HandePause);
    }

    private void Update()
    {
        _winConditions.Tick(Time.deltaTime);
        _loseConditions.Tick(Time.deltaTime);
    }

  public void TogglePause()
    {
        throw new System.NotImplementedException();
    }

    private void HandleWin()
    {
        Debug.Log("Win");
        _winConditions.PauseConditions(true);
        _loseConditions.PauseConditions(true);
        _onGameOver.Raise(true);
    }

    private void HanldeLose()
    {
        Debug.Log("Lose");
        _winConditions.PauseConditions(true);
        _loseConditions.PauseConditions(true);
        _onGameOver.Raise(false);
    }

    private void HandePause(bool isPaused)
    {
        _winConditions.PauseConditions(isPaused);
        _loseConditions.PauseConditions(isPaused);
    }

    private void HandleLevelPopulated(bool isPopulated)
    {
        _winConditions.StartConditions();
        _loseConditions.StartConditions();
    }
}
