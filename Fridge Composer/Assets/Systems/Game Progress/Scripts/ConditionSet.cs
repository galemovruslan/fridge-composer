using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Condition Set", menuName = "Conditions/Condition Set")]
public class ConditionSet : ScriptableObject
{
    [SerializeField] private List<Condition> _conditions;

    private HashSet<Condition> _satisfiedConditions = new HashSet<Condition>();

    private event Action _conditionsMet;

    private void OnEnable()
    {
        _satisfiedConditions.Clear();
        _conditions.ForEach((condition) => { condition.Subscribe(OnChangeConditionStatus); });
    }
    private void OnDisable()
    {
        _conditions.ForEach((condition) => { condition.Unsubscribe(OnChangeConditionStatus); });
    }

    public void StartConditions()
    {
        _conditions.ForEach( (condition) => { condition.StartChecking(); } );
    }

    public void PauseConditions()
    {
        _conditions.ForEach((condition) => { condition.PauseChecking(); });
    }

    public void Tick(float deltaTime)
    {
        _conditions.ForEach((condition) => { condition.Tick(deltaTime); });
    }

    public void AddListener(Action action)
    {
        _conditionsMet += action;
    }

    public void RemoveListener(Action action)
    {
        _conditionsMet -= action;
    }

    private void OnChangeConditionStatus(Condition condition, ConditionStatus conditionStatus)
    {
        switch (conditionStatus)
        {
            case ConditionStatus.fail:
                _satisfiedConditions.Remove(condition);
                break;
            case ConditionStatus.satisfy:
                _satisfiedConditions.Add(condition);
                break;
        }

        if (CheckAllConditionSatisfy())
        {
            _conditionsMet?.Invoke();
        }
    }

    private bool CheckAllConditionSatisfy()
    {
        return _satisfiedConditions.IsSupersetOf(_conditions);
    }
}