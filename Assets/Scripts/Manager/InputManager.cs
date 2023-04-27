using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
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
                        ICommand selectBlockCommand = new SelectBlockCommand(_gameManager, block);
                        selectBlockCommand.Execute();
                    }
                }
            }   
        }
    }
}
