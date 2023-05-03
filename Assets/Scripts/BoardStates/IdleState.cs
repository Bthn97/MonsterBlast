using UnityEngine;

public class IdleState : IBoardState
{
    private BoardStateManager _stateManager;
    private Camera _mainCamera;

    public IdleState(BoardStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public void EnterState()
    {
        Debug.LogFormat("Entered {0}", "Idle State");
        _mainCamera = Camera.main;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = _mainCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.collider != null)
                {
                    if (hit.transform.TryGetComponent(out Block block))
                    {
                        ICommand selectBlockCommand = new SelectBlockCommand(_stateManager, block);
                        selectBlockCommand.Execute();
                    }
                }
            }
        }
    }

    public void ExitState()
    {
       
    }
}

