using UnityEngine;

public class SelectBlockCommand : ICommand
{
    private Block _selectedBlock;
    private GameManager _gameManager;
    private Vector2 _worldPosition;

    public SelectBlockCommand(GameManager gameManager, Block selectedBlock)
    {
        _gameManager = gameManager;
        _selectedBlock = selectedBlock;
    }

    public void Execute()
    {
        _gameManager.HandleBlockSelection(_selectedBlock);
    }
}
