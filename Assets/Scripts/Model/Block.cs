using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Monster _monster;

    [SerializeField]
    private SpriteRenderer _monsterSprite;

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
            _monsterSprite.sprite = _monster.MonsterSprite;
        }
    }
}
