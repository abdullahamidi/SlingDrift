using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    [SerializeField]
    private float connectableCircleRadius;
    [SerializeField]
    private float driftSpeed;
    private StateMachine _stateMachine;
    private CircleCollider2D detectArea;
    //private BoxCollider2D driftArea;
    private LineRenderer slingLine;
    private SearchNode connectibleNode;
    private Rigidbody2D playerRb;
    private bool isFingerDown = false;
    private bool isPlayerInDriftArea = false;
    private bool isCollidedSomewhere = false;
    private bool pressedReplayButton = false;
    private bool isHookAttached = false;

    private void OnEnable()
    {
        InputManager.OnFingerDown += FingerDown;
        InputManager.OnFingerUp += FingerUp;
        SearchPlayerInDriftArea.OnPlayerEnterDriftArea += PlayerInDriftArea;
        SearchPlayerInDriftArea.OnPlayerExitDriftArea += PlayerOutDriftArea;
        ConnectToNode.OnHookAttached += HookAttached;
        Drift.OnHookReleased += HookReleased;
        CrashDetection.OnCrash += CarCrashed;
    }



    private void OnDisable()
    {
        InputManager.OnFingerDown -= FingerDown;
        InputManager.OnFingerUp -= FingerUp;
        SearchPlayerInDriftArea.OnPlayerEnterDriftArea -= PlayerInDriftArea;
        SearchPlayerInDriftArea.OnPlayerExitDriftArea -= PlayerOutDriftArea;
        ConnectToNode.OnHookAttached -= HookAttached;
        Drift.OnHookReleased -= HookReleased;
        CrashDetection.OnCrash -= CarCrashed;
    }



    private void Awake()
    {
        detectArea = GetComponentInChildren<CircleCollider2D>();
        detectArea.radius = connectableCircleRadius;

        slingLine = GetComponent<LineRenderer>();
        connectibleNode = gameObject.GetComponentInChildren<SearchNode>();
        playerRb = GetComponent<Rigidbody2D>();
        //detectArea = this.GetComponentInChildren<CircleCollider2D>();
        //detectArea.radius = connectableCircleRadius;
        //driftArea = this.GetComponentInChildren<CircleCollider2D>();
        _stateMachine = new StateMachine();

        var start = new Start();
        var moveTowards = new MoveTowards(this);
        var connectToNode = new ConnectToNode(this, connectibleNode, slingLine);
        var drift = new Drift(this, connectibleNode, slingLine, driftSpeed); //add particles
        var gameOver = new GameOver();

        At(start, moveTowards, ScreenTap());
        At(moveTowards, connectToNode, CanConnectToNode());
        At(connectToNode, drift, CanDrift());
        At(drift, moveTowards, ReleasedFinger());

        _stateMachine.AddAnyTransition(gameOver, Crashed());
        //At(gameOver, start, ReplayPressed());

        _stateMachine.SetState(start);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> ScreenTap() => () => isFingerDown;
        Func<bool> CanConnectToNode() => () => isFingerDown && connectibleNode.NodeFound;
        Func<bool> CanDrift() => () => isFingerDown && isPlayerInDriftArea && isHookAttached;
        Func<bool> ReleasedFinger() => () => !isFingerDown;
        Func<bool> Crashed() => () => isCollidedSomewhere;
        //Func<bool> ReplayPressed() => () => pressedReplayButton;

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
        isPlayerInDriftArea = false;
    }

    private void HookAttached()
    {
        isHookAttached = true;
        Debug.Log(isFingerDown + " " + isPlayerInDriftArea + " " + isHookAttached);
    }

    private void HookReleased()
    {
        isHookAttached = false;
    }

    private void CarCrashed()
    {
        isCollidedSomewhere = true;
    }
}
