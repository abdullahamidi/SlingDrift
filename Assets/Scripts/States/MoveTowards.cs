using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTowards : IState
{
    private Player _player;
    public MoveTowards(Player player)
    {
        _player = player;
    }

    public void FixedTick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        _player.transform.position += _player.transform.up * Time.deltaTime * _player.Speed;
    }
}
