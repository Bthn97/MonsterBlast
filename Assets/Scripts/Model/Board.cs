using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private int _rows = 8;
    [SerializeField]
    private int _columns = 8;

    [SerializeField]
    private Block _blockPrefab;
    [SerializeField]
    private List<Monster> _monsterTypes;

    private Block[,] _blocks;

    private void Start()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        _blocks = new Block[_rows, _columns];

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                Vector2Int gridPosition = new Vector2Int(i, j);

                var newBlockGO = ObjectPool.Instance.SpawnFromPool("Block", GetWorldPosition(gridPosition), Quaternion.identity);
                newBlockGO.transform.parent = transform;
                var newBlock = newBlockGO.GetComponent<Block>();

                newBlock.GridPosition = gridPosition;
                newBlock.Monster = GetRandomMonster();
                _blocks[i, j] = newBlock;
            }
        }
    }

    private Monster GetRandomMonster()
    {
        int randomIndex = Random.Range(0, _monsterTypes.Count);
        return _monsterTypes[randomIndex];
    }

    private Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x, gridPosition.y, 0);
    }

}
