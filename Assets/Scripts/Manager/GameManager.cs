using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Board _board;

    [SerializeField]
    private UIController _uiController;

    public int Score { get; private set; }

    private void AddScore(int points)
    {
        Score += points;
        _uiController.AddScore(points);
    }

}
