using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    public void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";
    }

}
