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
            Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            ICommand selectBlockCommand = new SelectBlockCommand(_gameManager, worldPosition);
            selectBlockCommand.Execute();
        }
    }
}
