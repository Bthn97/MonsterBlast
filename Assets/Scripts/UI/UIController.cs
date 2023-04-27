using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private UIView _uiView;

    private int _score;

    public void AddScore(int points)
    {
        _score += points;
        _uiView.UpdateScore(_score);
    }

}
