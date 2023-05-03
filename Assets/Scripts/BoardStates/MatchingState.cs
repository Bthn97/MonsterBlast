using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingState : IBoardState
{
    private BoardStateManager _stateManager;
    private int _rows, _columns;
    private Block[,] _blocks;
    private Block _selectedBlock;

    public MatchingState(BoardStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public void EnterState()
    {
        Debug.LogFormat("Entered {0}", "Matching State");
        _selectedBlock = _stateManager.SelectedBlock;
        _blocks = _stateManager.Blocks;
        _rows = _blocks.GetLength(0);
        _columns = _blocks.GetLength(1);

        
        var matchingBlocks = GetMatchingNeighbors(_selectedBlock, new List<Block>());

        if (matchingBlocks.Count >= 3)
        {
            _stateManager.MatchingBlocks = matchingBlocks;
            _stateManager.SetState(_stateManager.GetRemovingState());
        }
        else
        {
            _stateManager.MatchingBlocks.Clear();
            BoardUtilities.SetBlockMatch(matchingBlocks, false);
            _stateManager.SetState(_stateManager.GetIdleState());
        }
    }

    public void Update()
    {

    }

    public void ExitState()
    {

    }

    public List<Block> GetMatchingNeighbors(Block block, List<Block> visited = null)
    {
        if (visited == null)
        {
            visited = new List<Block>();
        }
        visited.Add(block);
        block.JuiceBlock();
        block.IsMatched = true;

        List<Block> matchingNeighbors = new List<Block>();
        List<Block> neighbors = BoardUtilities.GetNeighbors(_blocks, block, _rows, _columns);

        foreach (Block neighbor in neighbors)
        {
            if (!visited.Contains(neighbor) && neighbor.Monster.MonsterName == block.Monster.MonsterName)
            {
                matchingNeighbors.Add(neighbor);
                neighbor.IsMatched = true;
                neighbor.JuiceBlock();

                foreach (var item in GetMatchingNeighbors(neighbor, visited))
                {
                    if (!matchingNeighbors.Contains(item))
                    {
                        matchingNeighbors.Add(item);
                    }
                }
            }
        }
        matchingNeighbors.Add(block);

        return matchingNeighbors;
    }

    public IEnumerator RemoveAndReplace()
    {
        

        for (int x = 0; x < _columns; x++)
        {
            for (int y = 0; y < _rows; y++)
            {
                if (_blocks[x, y] != null && _blocks[x, y].IsMatched)
                {
                    _blocks[x, y].RemoveBlock();
                    yield return new WaitForSeconds(0.075f);
                }
            }
        }
        yield return null;
        BoardUtilities.SetBlockMatch(_blocks, false);

    }
}
