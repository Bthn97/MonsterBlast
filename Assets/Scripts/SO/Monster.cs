using UnityEngine;

[CreateAssetMenu(fileName = "MonsterType00",menuName = "Monsters",order = 1)]
public class Monster : ScriptableObject
{
    [SerializeField]
    private string _monsterName;

    [SerializeField]
    private Sprite _monsterSprite;

    [SerializeField]
    private Color _monsterColor;

    public string MonsterName => _monsterName;
    public Sprite MonsterSprite => _monsterSprite;
    public Color MonsterColor => _monsterColor;

}
