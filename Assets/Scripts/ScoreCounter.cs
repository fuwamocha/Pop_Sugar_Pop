using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] Text scoreText;
    int score;
    public void Add(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }
}
