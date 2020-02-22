using UnityEngine;

public class Platform : MonoBehaviour
{
    //When the player touches the platform, the player will be stuck with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.gameObject.transform.parent.SetParent(transform);
    }

    //When the player leaves the platform, the player will become unstuck to the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.gameObject.transform.parent.SetParent(null);
    }
}
