using UnityEngine;

public class Bone : MonoBehaviour
{
    private float speed = 550;
    private float rotation = 10;

    private void Start()
    {
        //As soon as the bone is thrown, it will only last 5 seconds
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        //This rotation could also be done with an animation
        transform.Rotate(new Vector3(0, 0, rotation));
    }

    public void Initialize(Vector2 direction)
    {
        //sets bone rotation to be clockwise if caveman was facing right and
        //counterclockwise if caveman was facing left
        rotation *= -direction.x;

        //Adds a force to the bone
        GetComponent<Rigidbody2D>().AddForce(direction.normalized * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When the bone hits an enemy, the bone will be destroyed
        Destroy(gameObject);
    }
}
