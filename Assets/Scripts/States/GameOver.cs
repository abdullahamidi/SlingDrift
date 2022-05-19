using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : IState
{
    public static Action OnGameOver;
    public void FixedTick()
    {
    }

    public void OnEnter()
    {
        Time.timeScale = 0;
        OnGameOver?.Invoke();
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
    }
}
