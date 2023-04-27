using UnityEngine;

public class SelectBlockCommand : ICommand
{
    private GameManager _gameManager;
    private Vector2 _worldPosition;

    public SelectBlockCommand(GameManager gameManager, Vector2 worldPosition)
    {
        _gameManager = gameManager;
        _worldPosition = worldPosition;
    }

    public void Execute()
    {
        _gameManager.HandleBlockSelection(_worldPosition);
    }
}
