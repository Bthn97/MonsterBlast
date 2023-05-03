using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBoardState
{
    private BoardStateManager _stateManager;

    public RemovingState(BoardStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public void EnterState()
    {
        Debug.LogFormat("Entered {0}", "RemovingState");

        var matchingBlocks = _stateManager.MatchingBlocks;
        matchingBlocks.ForEach(x => x.RemoveBlock());

        _stateManager.SetState(new FallingState(_stateManager)); 
    }

    public void ExitState()
    {
        
    }

    public void Update()
    {
        
    }
}
