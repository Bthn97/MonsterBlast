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


    public void HandleBlockSelection(Vector2 worldPosition)
    {       
        Block selectedBlock = _board.GetBlockAtWorldPosition(worldPosition);
        if (selectedBlock == null) return;

        List<Block> matchingBlocks = _board.GetMatchingNeighbors(selectedBlock);

        // Eşleşme sağlanırsa, puan ekleyin ve blokları yok edin
        if (matchingBlocks.Count >= 2) // 2 veya daha fazla komşu blok eşleşirse
        {
            AddScore(matchingBlocks.Count * 10);
            _board.RemoveAndReplaceBlocks(matchingBlocks);
        }
    }

    public void SwapBlocks(Block block1, Block block2)
    {
        // Check for matches!!
    }

    private void AddScore(int points)
    {
        Score += points;
        _uiController.AddScore(points);
    }

}
