using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Board _board;

    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private UIController _uiController;

    public int Score { get; private set; }


    public void HandleBlockSelection(Block _selectedBlock)
    {       
        if (_selectedBlock == null) return;

        List<Block> matchingBlocks = _board.GetMatchingNeighbors(_selectedBlock);

        if (matchingBlocks.Count >= 2)
        {
            AddScore(matchingBlocks.Count * 10);
            _board.RemoveAndReplaceBlocks(matchingBlocks);
        }
    }

    private void AddScore(int points)
    {
        Score += points;
        _uiController.AddScore(points);
    }

}
