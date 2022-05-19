using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToNode : IState
{
    public static Action OnHookAttached;

    private Player _player;
    private SearchNode _nodeSearcher;
    private Node _nodeToConnect;
    private LineRenderer _slingLine;
    private Vector3 hookPos;
    private float hookSpeed = 30f;
    public ConnectToNode(Player player, SearchNode nodeSearcher, LineRenderer slingLine)
    {
        _player = player;
        _nodeSearcher = nodeSearcher;
        _slingLine = slingLine;
    }

    public void FixedTick()
    {
    }

    public void OnEnter()
    {
        _nodeToConnect = _nodeSearcher.GetNodeInRange();
        _nodeSearcher.ActiveNode = _nodeToConnect;
        hookPos = _player.transform.position;
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        if (hookPos == _nodeToConnect.transform.position)
        {
            if (OnHookAttached != null)
                OnHookAttached();
        }
        hookPos = Vector3.MoveTowards(hookPos, _nodeToConnect.transform.position, Time.deltaTime * hookSpeed);
        _slingLine.SetPosition(0, _player.transform.position);
        _slingLine.SetPosition(1, hookPos);
        //move car
        _player.transform.position += _player.transform.up * Time.deltaTime * _player.Speed;
    }
}
