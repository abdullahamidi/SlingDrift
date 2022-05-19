using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : IState
{
    public static Action OnHookReleased;


    private Player _player;
    private SearchNode _nodeSearcher;
    private Node _connectedNode;
    private LineRenderer _slingLine;
    private float _driftSpeed;
    private float driftCircleRadius = 0f;
    private Vector3 hookPos;
    private float hookSpeed = 15f;
    private float moveDir;
    private float angle;
    private Vector3 origin;

    private TweenerCore<Quaternion, Quaternion, NoOptions> _driftRotationAdjustTween;
    private TweenerCore<Quaternion, Quaternion, NoOptions> _fixRotationTween;

    public Drift(Player player, SearchNode nodeSearcher, LineRenderer slingLine, float driftSpeed)
    {
        _player = player;
        _nodeSearcher = nodeSearcher;
        _slingLine = slingLine;
        _driftSpeed = driftSpeed;
    }

    public void FixedTick()
    {
    }

    public void OnEnter()
    {
        _connectedNode = _nodeSearcher.GetNode();
        origin = _connectedNode.transform.position;
        Vector3 playerPositionForNode = _player.transform.InverseTransformPoint(_connectedNode.transform.position);
        moveDir = playerPositionForNode.x < 0 ?
            1f : -1f;
        driftCircleRadius = Vector3.Distance(_player.transform.position, _connectedNode.transform.position);
        _player.Speed += 0.1f;
        //_player.transform.parent = _connectedNode.transform;
        //_driftRotationAdjustTween?.Kill();
        //_driftRotationAdjustTween = _player.transform.DOLocalRotateQuaternion(Quaternion.Euler(Vector3.forward * 60 * moveDir), 0.1f).SetLoops(1, LoopType.Incremental);
    }

    public void OnExit()
    {
        //will not work properly
        while (hookPos != _player.transform.position)
        {
            hookPos = Vector3.MoveTowards(hookPos, _player.transform.position, Time.deltaTime * hookSpeed); //make it linear
            _slingLine.SetPosition(1, hookPos);
        }
        //exit noktasýný geçmeden düzeltme yapmasýn 
        _fixRotationTween?.Kill();
        Vector3 roadRotation = _connectedNode.transform.parent.GetComponent<RoadTile>().GetRoadRotation();
        _fixRotationTween = _player.transform.DORotateQuaternion(Quaternion.Euler(roadRotation), 0.2f);
        //_player.transform.eulerAngles = _connectedNode.transform.parent.GetComponent<RoadTile>().GetRoadRotation();
        if (OnHookReleased != null) OnHookReleased();
        //_player.transform.parent = null;
        _driftRotationAdjustTween?.Kill();
    }

    public void Tick()
    {
        Vector3 targetDir = _connectedNode.transform.position - _player.transform.position;
        var carRotation = Quaternion.AngleAxis(60 * -moveDir, Vector3.forward);
        targetDir = carRotation * targetDir;

        _player.transform.up = Vector3.Lerp(_player.transform.up, targetDir, Time.deltaTime * 3);
        _player.transform.RotateAround(origin, _player.transform.forward * moveDir, 360 * _player.Speed / (2 * Mathf.PI * driftCircleRadius) * Time.deltaTime); // check again
        _slingLine.SetPosition(0, _player.transform.position);
    }
}
