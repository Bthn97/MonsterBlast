using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Board : MonoBehaviour
{
    private int _rows = 8;
    private int _columns = 8;
    private int _minMatches = 3;

    [SerializeField]
    private float _removeAnimDuration = 0.5f;

    [SerializeField]
    private float _blockSize = 5f;

    [SerializeField]
    private List<Monster> _monsterTypes;

    [SerializeField]
    private Monster _emptyMonster;

    private Block[,] _blocks;
    private Block[,] _bufferBlocks;
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
        _bufferBlocks = new Block[_rows, _columns];
        for (int x = 0; x < _columns; x++)
        {
            for (int y = 0; y < _rows; y++)
            {
                _bufferBlocks[x, y] = _blocks[x, y];
            }
        }
        

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

        List<Block> matchingNeighbors = new List<Block>();
        List<Block> neighbors = BoardUtilities.GetNeighbors(_blocks, block, _rows, _columns);

        foreach (Block neighbor in neighbors)
        {
            if (!visited.Contains(neighbor) && neighbor.Monster.MonsterName == block.Monster.MonsterName)
            {
                matchingNeighbors.Add(neighbor);
                neighbor.JuiceBlock();
                Debug.LogFormat("grid {0}", neighbor.Monster.MonsterName);
                matchingNeighbors.AddRange(GetMatchingNeighbors(neighbor, visited));
            }
        }

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
                    _bufferBlocks[x, y] = null;
                    _blocks[x, y].RemoveBlock();
                }
                else
                {
                    _bufferBlocks[x, y] = _blocks[x, y];
                }
            }
        }

        for (int x = 0; x < _columns; x++)
        {
            for (int y = 0; y < _rows; y++)
            {
                if (_bufferBlocks[x, y] == null)
                {
                    for (int yAbove = y + 1; yAbove < _rows; yAbove++)
                    {
                        if (_bufferBlocks[x, yAbove] != null)
                        {
                            Vector3 targetPosition = new Vector3(x, y, 0);
                            _bufferBlocks[x, yAbove].MoveBlock(targetPosition);
                            _bufferBlocks[x, y] = _bufferBlocks[x, yAbove];
                            _bufferBlocks[x, yAbove] = null;

                            _bufferBlocks[x, y].RemoveBlock();

                            break;
                        }
                    }
                }
            }
        }

        yield return new WaitForSeconds(_removeAnimDuration);

        for (int x = 0; x < _columns; x++)
        {
            for (int y = 0; y < _rows; y++)
            {
                _blocks[x, y] = _bufferBlocks[x, y];
            }
        }     
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
