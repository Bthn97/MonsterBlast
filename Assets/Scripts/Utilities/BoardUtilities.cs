using System.Collections.Generic;
using UnityEngine;

public static class BoardUtilities
{
    public static List<Block> GetNeighbors(Block[,] board, Block block, int rows, int columns)
    {
        List<Block> neighbors = new List<Block>();
        int x = block.GridPosition.x;
        int y = block.GridPosition.y;

        // Left neighbor
        if (x > 0)
            neighbors.Add(board[x - 1, y]);

        // Right neighbor
        if (x < columns - 1)
            neighbors.Add(board[x + 1, y]);

        // Bottom neighbor
        if (y > 0)
            neighbors.Add(board[x, y - 1]);

        // Top neighbor
        if (y < rows - 1)
            neighbors.Add(board[x, y + 1]);

        return neighbors;
    }

    public static void SetBlockMatch(Block[,] board,bool isMatched)
    {
        var x = board.GetLength(0);
        var y = board.GetLength(1);

        for (int i = 0; i < x; i++)
        {
            for (int m = 0; m < y; m++)
            {
                board[i, m].IsMatched = isMatched;
            }
        }

    }

    public static void SetBlockMatch(List<Block> blocks, bool isMatched)
    {
        foreach (var item in blocks)
            item.IsMatched = isMatched;
    }
}
