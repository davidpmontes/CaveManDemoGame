using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private int score;

    void Start()
    {
        Instance = this;
        AudioManager.Instance.PlayWithoutOverlap("level1");
    }

    public void CoinCollected(int value)
    {
        score += value;
        CanvasManager.Instance.SetScore(score);
    }

    public void FinishLineReached()
    {
        AudioManager.Instance.Stop("level1");
        SceneManager.LoadScene("MainMenu");
    }
}
