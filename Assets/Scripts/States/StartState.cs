using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    public static Action OnStart;
    public StartState()
    {
    }

    public void FixedTick()
    {

    }

    public void OnEnter()
    {
        Time.timeScale = 1;
    }

    public void OnExit()
    {
        OnStart?.Invoke();
    }

    public void Tick()
    {
    }
}
