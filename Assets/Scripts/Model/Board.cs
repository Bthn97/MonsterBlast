using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private int _rows = 8;
    [SerializeField]
    private int _columns = 8;

    [SerializeField]
    private float _blockSize = 5f;

    [SerializeField]
    private List<Monster> _monsterTypes;

    private Block[,] _blocks;

    private void OnEnable()
    {
        BlockSpawner.OnBlocksCreated += GetBlocks;
    }
    private void OnDisable()
    {
        BlockSpawner.OnBlocksCreated -= GetBlocks;
    }

    private void GetBlocks(Block[,] createdBlocks) => _blocks = createdBlocks;

    public Block GetBlockAtWorldPosition(Vector2 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / _blockSize);
        int y = Mathf.FloorToInt(worldPosition.y / _blockSize);

        if (x < 0 || x >= _columns || y < 0 || y >= _rows) return null;

        return _blocks[x, y];
    }

    public List<Block> GetMatchingNeighbors(Block block)
    {
        return null;
    }

    public void RemoveAndReplaceBlocks(List<Block> matchingBlocks)
    {
        
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
