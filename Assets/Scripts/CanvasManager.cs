using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private Text score;

    public static CanvasManager Instance;

    void Start()
    {
        Instance = this;
    }

    public void SetScore(int value)
    {
        score.text = string.Format("Score: {0}", value);
    }
}
