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
        //playerRb.AddRelativeForce(_player.transform.up * _player.Speed);
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

    private void RotateTowards()
    {
        if (_player.transform.eulerAngles.z % 90 == 0)
            return;

        if (_player.transform.eulerAngles.z < 90 && _player.transform.eulerAngles.z > 0)
        {
            _player.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (_player.transform.eulerAngles.z < 180 && _player.transform.eulerAngles.z > 90)
        {
            _player.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (_player.transform.eulerAngles.z < 270 && _player.transform.eulerAngles.z > 180)
        {
            _player.transform.eulerAngles = new Vector3(0, 0, 270);
        }
        else
        {
            _player.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
