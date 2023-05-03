using System.Collections.Generic;
using UnityEngine;

public class BoardStateManager : MonoBehaviour
{
    public IBoardState CurrentState { get; private set; }
    public Block[,] Blocks { get; set; }
    public List<Block> MatchingBlocks { get; set; }
    public Board BoardScript;
    public Block SelectedBlock;

    private readonly IBoardState _initState;
    private readonly IBoardState _idleState;
    private readonly IBoardState _matchingState;
    private readonly IBoardState _removingState;
    private readonly IBoardState _replacingState;

    public BoardStateManager()
    {
        _initState = new InitState(this);
        _idleState = new IdleState(this);
        _matchingState = new MatchingState(this);
        _removingState = new RemovingState(this);
        _replacingState = new ReplacingState(this);
    }

    private void Start()
    {
        SetState(new InitState(this));
    }

    private void Update()
    {
        CurrentState.Update();
    }

    public void SetState(IBoardState newState)
    {
        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }

    public IBoardState GetInitState() => _initState;
    public IBoardState GetIdleState() => _idleState;
    public IBoardState GetMatchingState() => _matchingState;
    public IBoardState GetRemovingState() => _removingState;
    public IBoardState GetReplacingState() => _replacingState;
}
