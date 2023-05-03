using UnityEngine;

public class FallingState : IBoardState
{
    private BoardStateManager _stateManager;

    public FallingState(BoardStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public void EnterState()
    {
        Debug.LogFormat("Entered {0}", "FallingState");
    }

    public void Update()
    {

    }

    public void ExitState()
    {

    }
}
