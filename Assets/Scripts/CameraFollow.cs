using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 1;


    void LateUpdate()
    {
        //This is constantly tracking the target's x position and moving the camera's x position to match
        //The y position never changes
        //The z position never changes
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x,
                                                           target.position.x,
                                                           Mathf.Abs(transform.position.x - target.position.x) * speed * Time.deltaTime),
                                                           transform.position.y,
                                                           transform.position.z);
    }
}
