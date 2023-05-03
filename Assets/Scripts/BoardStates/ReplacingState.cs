using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacingState : IBoardState
{
    private BoardStateManager _stateManager;

    public ReplacingState(BoardStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public void EnterState()
    {
        Debug.LogFormat("Entered {0}", "Replacing State");
    }

    public void ExitState()
    {
        
    }

    public void Update()
    {
        
    }
}
