using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] TimerCondition _timer;
    [SerializeField] TextMeshProUGUI _text;

    private void OnEnable()
    {
        _timer.SubscribeOnEvents(SetTime);
    }

    public void SetTime(float secondsFromStart)
    {
        int minutes = (int) secondsFromStart / 60;
        int seconds = (int) secondsFromStart % 60;
        _text.text = $"{minutes:D2}:{seconds:D2}";
    }

}
