using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField] private BoolGameEvent _pauseEvent;

    private bool isPaused;

    public void Pause()
    {
        SetPause(true);
    }

    public void Unpause()
    {
        SetPause(false);        
    }

    private void SetPause(bool state)
    {
        isPaused = state;
        _pauseEvent.Raise(isPaused);
    }
}
