using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    private int _rows = 6;
    private int _columns = 6;

    [SerializeField]
    private float _removeAnimDuration = 0.5f;

    [SerializeField]
    private List<Monster> _monsterTypes;

    [SerializeField]
    private Monster _emptyMonster;

    private BoardStateManager _boardStateManager;
    private Block[,] _blocks;
    private bool isBoardProcessing = false;
    public static event Action OnBoardUpdated;

    private void Awake()
    {
        _boardStateManager = GetComponent<BoardStateManager>();
    }

    public Monster GetRandomMonster()
    {
        int randomIndex = UnityEngine.Random.Range(0, _monsterTypes.Count);
        return _monsterTypes[randomIndex];
    }

    private Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x, gridPosition.y, 0);
    }

}
