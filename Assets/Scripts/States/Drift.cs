using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : IState
{
    public static Action<Node> OnHookReleased;


    private Player _player;
    private SearchNode _nodeSearcher;
    private Node _connectedNode;
    private LineRenderer _slingLine;
    private float driftCircleRadius = 0f;
    private Vector3 hookPos;
    private float hookSpeed = 15f;
    private float moveDir;
    private Vector3 origin;
    private float angleBetweenCarAndHook = 45;


    public Drift(Player player, SearchNode nodeSearcher, LineRenderer slingLine)
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
        _connectedNode = _nodeSearcher.ActiveNode;
        origin = _connectedNode.transform.position;
        Vector3 playerPositionForNode = _player.transform.InverseTransformPoint(_connectedNode.transform.position);
        moveDir = playerPositionForNode.x < 0 ?
            1f : -1f;
        driftCircleRadius = Vector3.Distance(_player.transform.position, _connectedNode.transform.position);
        _player.Speed += 0.1f;
    }

    public void OnExit()
    {
        //will not work properly
        while (hookPos != _player.transform.position)
        {
            hookPos = Vector3.MoveTowards(hookPos, _player.transform.position, Time.deltaTime * hookSpeed);
            _slingLine.SetPosition(1, hookPos);
        }

        OnHookReleased?.Invoke(_connectedNode);
    }

    public void Tick()
    {
        //tekrar bak
        Vector3 targetDir = _connectedNode.transform.position - _player.transform.position;
        var carRotation = Quaternion.AngleAxis(angleBetweenCarAndHook * -moveDir, Vector3.forward);
        targetDir = carRotation * targetDir;

        _player.transform.up = Vector3.Lerp(_player.transform.up, targetDir, Time.deltaTime * 3);
        _player.transform.RotateAround(origin, _player.transform.forward * moveDir, 360 * _player.Speed / (2 * Mathf.PI * driftCircleRadius) * Time.deltaTime);
        _slingLine.SetPosition(0, _player.transform.position);
    }
}
