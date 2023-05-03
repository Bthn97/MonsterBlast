using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InitState : IBoardState
{
    private BoardStateManager _stateManager;
    public static event Action OnBoardInit;

    public InitState(BoardStateManager stateManager)
    {
        _stateManager = stateManager;
    }


    public void EnterState()
    {
        BlockSpawner.OnBlocksCreated += GetBlocks;
        OnBoardInit?.Invoke();
    }

    public void Update()
    {

    }

    public void ExitState()
    {       
        BlockSpawner.OnBlocksCreated -= GetBlocks;
        //_stateManager.SetState(new IdleState(_stateManager));
    }

    private void GetBlocks(Block[,] createdBlocks)
    {
        _stateManager.Blocks = createdBlocks;
        GenerateMonsters();
    }

    private void GenerateMonsters()
    {
        var _blocks = _stateManager.Blocks;
        var _rows = _blocks.GetLength(0);
        var _columns = _blocks.GetLength(1);

        for (int x = 0; x < _rows; x++)
        {
            for (int y = 0; y < _columns; y++)
            {
                _blocks[x, y].GridPosition = new Vector2Int(x, y);
                _blocks[x, y].Monster = _stateManager.BoardScript.GetRandomMonster();
            }
        }
        _stateManager.SetState(new IdleState(_stateManager));
    }
}
