using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Tell the levelmanager that the player collected something worth 100 points
        LevelManager.Instance.CoinCollected(100);
        AudioManager.Instance.PlayOverlapping("coin");

        //Triggers the coins animation when collected
        GetComponent<Animator>().SetTrigger("selected");

        //disable the coin from being triggered again as it is disappearing
        GetComponent<CircleCollider2D>().enabled = false;

        //destroy the gameobject in 1 second
        Destroy(transform.parent.gameObject, 1);
    }
}
