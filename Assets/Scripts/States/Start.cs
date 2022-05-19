using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : IState
{
    public Start()
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
    }

    public void Tick()
    {
    }
}
