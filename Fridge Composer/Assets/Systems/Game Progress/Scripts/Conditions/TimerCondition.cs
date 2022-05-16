using System;
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

    public void SubscribeOnEvents(Action<float> onUpdateTimer)
    {
        _progress.SubscribeOnEvents(onUpdateTimer);
    }

    public override void StartChecking()
    {
        _progress.Start(_presetValue);
        Debug.Log("Start timer");
    }

    public override void PauseChecking(bool isPaused)
    {
        _progress.TogglePause(isPaused);
    }

    public override void Tick(float deltaTime)
    {
        _progress.Tick(deltaTime);
    }

    private void SatisfyCondition()
    {
        ChangeStatus(this, ConditionStatus.satisfy);
    }


}
