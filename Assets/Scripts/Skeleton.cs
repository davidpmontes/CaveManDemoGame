using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skeleton : MonoBehaviour//, IEnemy
{
    private int life = 5;
    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;

    void Start()
    {
        //Cache the SpriteRenderer because we change the color to make the enemy flash when receiving damage
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Cache the starting position so the enemy can oscillate about this position
        startPosition = transform.position;
    }

    void Update()
    {
        //This is an alternative way to move the enemy in a periodic motion.  Another way would be animation
        transform.position = new Vector3(transform.position.x, startPosition.y + 1 * Mathf.Sin(Time.time * 5), transform.position.z);
    }

    //This function is called when it detects a collision.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //The player dies and restarts if he touches an enemy
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene("level1");

        life -= 1;
        StartCoroutine(FlashRed(2));
        AudioManager.Instance.PlayOverlapping("damage");

        if (life <= 0)
        {
            AudioManager.Instance.PlayOverlapping("die");
            Destroy(gameObject);
        }
    }

    //This is a special function that can run concurrently so this is good when you want
    //some function to run for an extended period of time without halting your entire game.
    IEnumerator FlashRed(int numLoops)
    {
        for (int i = 0; i < numLoops; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
