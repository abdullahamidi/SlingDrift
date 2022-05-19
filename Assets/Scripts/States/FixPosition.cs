using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPosition : IState
{
    public static Action OnPositionFix;

    private Player _player;
    private Node _connectedNode;
    private SearchNode _nodeSearcher;
    private TweenerCore<Quaternion, Quaternion, NoOptions> _fixRotationTween;
    public FixPosition(Player player, SearchNode nodeSearcher)
    {
        _player = player;
        _nodeSearcher = nodeSearcher;
    }
    public void FixedTick()
    {
    }

    public void OnEnter()
    {
        _connectedNode = _nodeSearcher.ActiveNode;
        _fixRotationTween?.Kill();
        Vector3 roadRotation = _connectedNode.transform.parent.GetComponent<RoadTile>().GetRoadRotation();
        _fixRotationTween = _player.transform.DORotateQuaternion(Quaternion.Euler(roadRotation), 0.2f);
        OnPositionFix?.Invoke();
    }

    public void OnExit()
    {
        _nodeSearcher.ActiveNode = null;
    }

    public void Tick()
    {
    }
}
