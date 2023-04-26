using System.Threading;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Monster _monster;

    public Monster Monster
    {
        get => _monster;
        set
        {
            _monster = value;
            UpdateAppearance();
        }
    }

    public Vector2Int GridPosition { get; set; }

    private void UpdateAppearance()
    {
        if (_monster != null)
        {
            // Update block appearance based on the assigned monster (e.g. change sprite, color, etc.)
        }
    }
}
