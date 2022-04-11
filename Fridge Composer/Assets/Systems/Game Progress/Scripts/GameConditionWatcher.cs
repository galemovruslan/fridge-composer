using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConditionWatcher : MonoBehaviour, IPauseable
{

    [SerializeField] private ConditionSet _winConditions;
    [SerializeField] private ConditionSet _loseConditions;
    [SerializeField] private BoolGameEvent _onPause;
    private void OnEnable()
    {
        _winConditions.AddListener(HandleWin);
        _winConditions.StartConditions();

        _loseConditions.AddListener(HanldeLose);
        _loseConditions.StartConditions();

        _onPause.AddListener(HandePause);
    }

    private void OnDisable()
    {
        _winConditions.RemoveListener(HandleWin);
        _loseConditions.RemoveListener(HanldeLose);
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
    }

    private void HanldeLose()
    {
        Debug.Log("Lose");
        _winConditions.PauseConditions(true);
        _loseConditions.PauseConditions(true);
    }

    private void HandePause(bool isPaused)
    {
        _winConditions.PauseConditions(isPaused);
        _loseConditions.PauseConditions(isPaused);
    }
}
