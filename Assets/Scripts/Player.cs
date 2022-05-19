using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    [SerializeField]
    private float connectableCircleRadius;
    private StateMachine _stateMachine;
    private CircleCollider2D detectArea;
    private LineRenderer slingLine;
    private SearchNode connectibleNode;
    private bool isFingerDown = false;
    private bool isPlayerInDriftArea = false;
    private bool isCollidedSomewhere = false;
    private bool isHookAttached = false;
    private bool isPosFixed = false;

    private void OnEnable()
    {
        InputManager.OnFingerDown += FingerDown;
        InputManager.OnFingerUp += FingerUp;
        SearchPlayerInDriftArea.OnPlayerEnterDriftArea += PlayerInDriftArea;
        PlayerExitDriftArea.OnPlayerExitDriftArea += PlayerOutDriftArea;
        ConnectToNode.OnHookAttached += HookAttached;
        Drift.OnHookReleased += HookReleased;
        CrashDetection.OnCrash += CarCrashed;
        FixPosition.OnPositionFix += CarPosFixed;
    }



    private void OnDisable()
    {
        InputManager.OnFingerDown -= FingerDown;
        InputManager.OnFingerUp -= FingerUp;
        SearchPlayerInDriftArea.OnPlayerEnterDriftArea -= PlayerInDriftArea;
        PlayerExitDriftArea.OnPlayerExitDriftArea -= PlayerOutDriftArea;
        ConnectToNode.OnHookAttached -= HookAttached;
        Drift.OnHookReleased -= HookReleased;
        CrashDetection.OnCrash -= CarCrashed;
        FixPosition.OnPositionFix -= CarPosFixed;
    }



    private void Awake()
    {
        detectArea = GetComponentInChildren<CircleCollider2D>();
        detectArea.radius = connectableCircleRadius;

        slingLine = GetComponent<LineRenderer>();
        connectibleNode = gameObject.GetComponentInChildren<SearchNode>();
        _stateMachine = new StateMachine();

        var start = new Start();
        var moveTowards = new MoveTowards(this);
        var connectToNode = new ConnectToNode(this, connectibleNode, slingLine);
        var drift = new Drift(this, connectibleNode, slingLine); //add particles
        var fixPosition = new FixPosition(this, connectibleNode);
        var gameOver = new GameOver();

        At(start, moveTowards, ScreenTap());
        At(moveTowards, connectToNode, CanConnectToNode());
        At(connectToNode, drift, CanDrift());
        At(drift, fixPosition, OutOfDriftAreaAndReleasedFinger());
        At(drift, moveTowards, InDriftAreaAndReleasedFinger());
        At(fixPosition, moveTowards, PositionFixed());
        _stateMachine.AddAnyTransition(gameOver, Crashed());

        _stateMachine.SetState(start);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> ScreenTap() => () => isFingerDown;
        Func<bool> CanConnectToNode() => () => isFingerDown && connectibleNode.NodeFound;
        Func<bool> CanDrift() => () => isFingerDown && isPlayerInDriftArea && isHookAttached;
        Func<bool> OutOfDriftAreaAndReleasedFinger() => () => !isFingerDown && !isPlayerInDriftArea;
        Func<bool> PositionFixed() => () => isPosFixed;
        Func<bool> InDriftAreaAndReleasedFinger() => () => !isFingerDown && isPlayerInDriftArea;
        Func<bool> Crashed() => () => isCollidedSomewhere;

    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedTick();
    }

    private void FingerDown()
    {
        Debug.Log("barnahýmý býraktým");
        isFingerDown = true;
    }

    private void FingerUp()
    {
        isFingerDown = false;
    }
    private void PlayerInDriftArea()
    {
        isPlayerInDriftArea = true;
    }

    private void PlayerOutDriftArea()
    {
        Debug.Log("çýktým");
        isPlayerInDriftArea = false;
    }

    private void HookAttached()
    {
        isHookAttached = true;
    }

    private void HookReleased(Node activeNode)
    {
        isHookAttached = false;
        isPosFixed = false;
        connectibleNode.ActiveNode = activeNode;
    }

    private void CarCrashed()
    {
        isCollidedSomewhere = true;
    }

    private void CarPosFixed()
    {
        isPosFixed = true;
    }
}
