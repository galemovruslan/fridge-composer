using System;

public class TimerProgress 
{
    private float _timerValue;
    private float _presetValue;
    private event Action _onTimerEnd;
    private bool _isPaused = false;

    public TimerProgress( Action onTimerEnd)
    {
        _onTimerEnd += onTimerEnd;
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

        if(_timerValue <= _presetValue)
        {
            _onTimerEnd?.Invoke();
        }
    }

}
