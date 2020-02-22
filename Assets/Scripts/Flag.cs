using UnityEngine;

public class Flag : MonoBehaviour
{
    //When the player reaches the flag, the flag tells the level manager that the level is finished.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.Instance.FinishLineReached();
    }
}
