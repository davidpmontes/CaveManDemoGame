using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayWithoutOverlap("mainMenu");
    }

    public void OnPlayButtonClicked()
    {
        AudioManager.Instance.Stop("mainMenu");
        SceneManager.LoadScene("Level1");
    }
}
