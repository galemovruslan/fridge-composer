using System;
using UnityEngine;

public abstract class Condition : ScriptableObject
{

    private event Action<Condition, ConditionStatus> _onStatusChange;

    public abstract void StartChecking();
    public virtual void PauseChecking(bool isPaused) { }

    public abstract void Tick(float deltaTime);

    public void Subscribe(Action<Condition, ConditionStatus> action)
    {
        _onStatusChange += action;
    }

    public void Unsubscribe(Action<Condition, ConditionStatus> action)
    {
        _onStatusChange -= action;
    }

    protected void ChangeStatus(Condition condition, ConditionStatus conditionStatus)
    {
        _onStatusChange.Invoke(condition, conditionStatus);
    }

}

public enum ConditionStatus
{
    fail,
    satisfy
}