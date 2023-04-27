using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Board : MonoBehaviour
{
    private int _rows = 8;
    private int _columns = 8;

    [SerializeField]
    private float _removeAnimDuration = 0.5f;

    [SerializeField]
    private List<Monster> _monsterTypes;

    [SerializeField]
    private Monster _emptyMonster;

    private Block[,] _blocks;
    private bool isBoardProcessing = false;
    public static event Action OnBoardUpdated;

    private void OnEnable()
    {
        BlockSpawner.OnBlocksCreated += GetBlocks;
    }

    private void OnDisable()
    {
        BlockSpawner.OnBlocksCreated -= GetBlocks;
    }

    private void GetBlocks(Block[,] createdBlocks)
    {
        _blocks = createdBlocks;
        GenerateMonsters();  
    }

    private void GenerateMonsters()
    {
        _rows = _blocks.GetLength(0);
        _columns = _blocks.GetLength(1);

        for (int x = 0; x < _rows; x++)
        {
            for (int y = 0; y < _columns; y++)
            {
                _blocks[x, y].GridPosition = new Vector2Int(x, y);
                _blocks[x, y].Monster = GetRandomMonster();
            }
        }
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
                //matchingNeighbors.AddRange(GetMatchingNeighbors(neighbor, visited));
            }
        }
        matchingNeighbors.Add(block);

        return matchingNeighbors;
    }


    public void RemoveAndReplaceBlocks(List<Block> matchingBlocks)
    {
        StartCoroutine(RemoveAndReplace());
    }

    private IEnumerator RemoveAndReplace()
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

    private Monster GetRandomMonster()
    {
        int randomIndex = UnityEngine.Random.Range(0, _monsterTypes.Count);
        return _monsterTypes[randomIndex];
    }

    private Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x, gridPosition.y, 0);
    }

}
