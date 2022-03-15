using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConditionWatcher : MonoBehaviour
{

    [SerializeField] private ConditionSet _winConditions;
    [SerializeField] private ConditionSet _loseConditions;

    private void OnEnable()
    {
        _winConditions.AddListener(HandleWin);
        _winConditions.StartConditions();

        _loseConditions.AddListener(HanldeLose);
        _loseConditions.StartConditions();
    }

    private void OnDisable()
    {
        _winConditions.RemoveListener(HandleWin);
        _loseConditions.RemoveListener(HanldeLose);
    }

    private void Update()
    {
        _winConditions.Tick(Time.deltaTime);
        _loseConditions.Tick(Time.deltaTime);
    }

    private void HandleWin()
    {
        Debug.Log("Win");
        _winConditions.PauseConditions();
    }

    private void HanldeLose()
    {
        Debug.Log("Lose");
        _loseConditions.PauseConditions();
    }
}
