using UnityEngine;

public class Caveman : MonoBehaviour
{
    public GameObject bonePrefab;

    private Rigidbody2D rb2d;
    private Animator animator;

    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool attackInput;

    private bool isGrounded = true;
    private bool canJump;
    private float attackTimer;

    private readonly float RUN_SPEED = 5;
    private readonly float JUMP_STRENGTH = 15;

    void Start()
    {
        //this is caching references to components that you will be regularly accessing
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //Update occurs every frame so 60 times a second for a 60 frame per second game
    void Update()
    {
        GetInput();
        GroundCheck();
        Animations();
        Flip();
        Attack();
    }

    //Use fixed update when manipulating physics objects like the rigidbody
    void FixedUpdate()
    {
        Run();
        Jump();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetKey(KeyCode.K);
        attackInput = Input.GetKey(KeyCode.J);
    }

    private void Run()
    {
        //only adds a horizontal velocity component, leaves the vertical velocity untouched
        rb2d.velocity = new Vector2(horizontalInput * RUN_SPEED, rb2d.velocity.y);
    }

    private void GroundCheck()
    {
        //sets the layermask so that rays only detect the "Solids" layer
        LayerMask layermask = 1 << LayerMask.NameToLayer("Solids");

        RaycastHit2D[] rays = new RaycastHit2D[3];
        rays[0] = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, layermask);
        rays[1] = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0), Vector2.down, 1.1f, layermask);
        rays[2] = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0), Vector2.down, 1.1f, layermask);

        //These show a visual representation of the raycasts
        //Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(-0.5f, 0), Vector2.down * 1.1f, Color.white);
        //Debug.DrawRay(transform.position + new Vector3(0.5f, 0), Vector2.down * 1.1f, Color.blue);

        //Check all rays to see if they encountered the ground
        for (int i = 0; i < rays.Length; i++)
        {
            if (rays[i].collider)
            {
                //A reflected surface normal of 0.9 means the ground must be 90% level ie not a wall.  
                if (rays[i].normal.y > 0.9f)
                {

                    isGrounded = true;

                    //Only enable jumping if you touch the ground and let go of jump button
                    if (!jumpInput)
                        canJump = true;

                    return;
                }
            }
        }

        //since we reached this point, we know none of the surface normals were 0.9 long so none touch ground so we must be airborne
        isGrounded = false;
    }

    private void Animations()
    {
        //sets the speed variable to the magnitude of your velocity, otherwise, a negative velocity won't trigger the animation change
        animator.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
        animator.SetBool("isGrounded", isGrounded);
    }

    //Flips the sprite left or right depending on your direction of movement
    private void Flip()
    {
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Jump()
    {
        //You must be on the ground to jump
        if (!isGrounded)
            return;

        //You can't hold down the jump button and keep jumping
        if (!canJump)
            return;

        //Finally, if you are pressing the jump button, you will jump!
        if (jumpInput)
        {
            //disables double jumping
            canJump = false;

            //adds only vertical velocity, leaves the horizontal velocity untouched
            rb2d.velocity = new Vector2(rb2d.velocity.x, JUMP_STRENGTH);

            //play a cool jumping sound
            AudioManager.Instance.PlayWithoutOverlap("jump");
        }
    }

    private void Attack()
    {
        //only throw a bone if pressing the attack button and enough time has passed since last attack
        if (attackInput && attackTimer < Time.time)
        {
            //Separates attacks by 0.2 seconds
            attackTimer = Time.time + 0.2f;

            //creates a new bone based on the prefab
            GameObject bone = Instantiate(bonePrefab);

            //sets the direction of the bone based on the direction player is facing
            bone.GetComponent<Bone>().Initialize(new Vector2(-transform.localScale.x, 1));

            //sets the starting position of the bone to caveman's starting position
            bone.transform.position = transform.position;

            //play a cool throwing sound
            AudioManager.Instance.PlayOverlapping("throw");
        }
    }
}
