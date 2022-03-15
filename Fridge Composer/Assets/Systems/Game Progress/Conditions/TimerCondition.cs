using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Timer Condition", menuName = "Conditions/Timer")]
public class TimerCondition : Condition
{
    [SerializeField] private float _presetValue;

    private TimerProgress _progress;

    private void OnEnable()
    {
        _progress = new TimerProgress(SatisfyCondition);
    }

    private void OnDisable()
    {
        _progress = null; 
    }

    private void SatisfyCondition()
    {
        ChangeStatus(this, ConditionStatus.satisfy);
    }

    public override void StartChecking()
    {
        _progress.Start(_presetValue);
    }

    public override void PauseChecking()
    {
        _progress.TogglePause();
    }

    public override void Tick (float deltaTime)
    {
        _progress.Tick(deltaTime);
    }
}
