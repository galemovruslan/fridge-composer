using System;

public class TimerProgress 
{
    private float _timerValue;
    private float _presetValue;
    private event Action _onTimerEnd;
    private event Action<float> _onTimerUpdate;
    private bool _isPaused = false;

    public TimerProgress( Action onTimerEnd)
    {
        _onTimerEnd += onTimerEnd;
    }

    public void SubscribeOnEvents(Action<float> onUpdateTimer)
    {
        _onTimerUpdate += onUpdateTimer;
    }

    public void Start(float presetValue)
    {
        _timerValue = presetValue;
        _presetValue = 0f;
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
    }

    public void Tick(float deltaTime)
    {
        if(_isPaused) 
        {
            return; 
        }

        _timerValue -= deltaTime;

        _onTimerUpdate?.Invoke(_timerValue);
        if (_timerValue <= _presetValue)
        {
            _onTimerEnd?.Invoke();
        }
    }

}
