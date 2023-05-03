using UnityEngine;

public class SelectBlockCommand : ICommand
{
    private Block _selectedBlock;
    //private GameManager _gameManager;
    private BoardStateManager _stateManager;
    private Vector2 _worldPosition;
    /*
    public SelectBlockCommand(GameManager gameManager, Block selectedBlock)
    {
        _gameManager = gameManager;
        _selectedBlock = selectedBlock;
    }*/
    public SelectBlockCommand(BoardStateManager stateManager, Block selectedBlock)
    {
        _stateManager = stateManager;
        _selectedBlock = selectedBlock;
    }

    public void Execute()
    {
        //_gameManager.HandleBlockSelection(_selectedBlock);
        _stateManager.SelectedBlock = _selectedBlock;
        _stateManager.SetState(new MatchingState(_stateManager));
    }
}
