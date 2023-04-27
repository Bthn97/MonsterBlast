using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Monster _monster;
    [SerializeField]
    private Monster _emptyMonster;
    [SerializeField]
    private SpriteRenderer _monsterSprite;
    [SerializeField]
    private float _moveAnimTime = 0.75f;
    [SerializeField]
    private float _moveAnimDistance = 2f;
    [SerializeField]
    private Ease _moveAnimEase = Ease.Linear;
    public bool IsMatched = false;

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

    public void JuiceBlock()
    {
        _monsterSprite.transform.DOShakeScale(0.2f,0.5f,1,10);
    }

    private void UpdateAppearance(bool isAnimated = true)
    {
        if (_monster != null)
        {
            if(isAnimated)
                MoveSprite(_monsterSprite.transform, _moveAnimDistance);
            _monsterSprite.sprite = _monster.MonsterSprite;
        }
    }

    public void RemoveBlock()
    {
        Monster = _emptyMonster;
    }

    public void MoveBlock(Vector3 targetPos)
    {

    }

    private void MoveSprite(Transform _renderer,float _fromHeight)
    {
        Vector3 defaultPos = _renderer.position;
        _renderer.position += Vector3.up * _fromHeight;
        _renderer.DOMove(defaultPos, _moveAnimTime)
            .SetEase(_moveAnimEase);
    }
}
