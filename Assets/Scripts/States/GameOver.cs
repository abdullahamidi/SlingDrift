using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : IState
{
    public void FixedTick()
    {
    }

    public void OnEnter()
    {
        Time.timeScale = 0;
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}
