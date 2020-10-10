using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 2.5f;
    [SerializeField] float climbSpeed = 5f;

    BoxCollider2D myFeetCollider;

    // TODO change these two to serialize field
    public float groundCheckRadius = 0.5f;
    public LayerMask ground;

    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D myBodyCollider;
    public bool CanClimb { get; set; } = false;
    float initialGravityScale;
    bool isDead = false;
    PersistData persistData = null;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        persistData = FindObjectOfType<PersistData>();
    }

    void Start()
    {
        initialGravityScale = rb.gravityScale;

        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
        if (enemy != null)
        {
            // makes sure we cant jump using the enemy
            Physics2D.IgnoreCollision(myFeetCollider, enemy.GetComponent<Collider2D>());
        }
    }

    void Update()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Last time I checked, these calls don't conflict with
        // one another, but if you spot something, let the team know
        Run();
        Jump();
        Climb();
        FlipSprite();
    }

    private void Die()
    {
        print("triggered");
        anim.SetBool("playerIsDead", isDead);
        persistData.TakeLives();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy")
            || myBodyCollider.IsTouchingLayers(LayerMask.GetMask("hazards")))
        {
            print("player died");
            isDead = true;
            Physics2D.IgnoreCollision((Collider2D) myBodyCollider, collision.collider);
            Physics2D.IgnoreCollision((Collider2D) myFeetCollider, collision.collider);
            Die();
        }
    }

    private void Jump()
    {
        // we are only allowed to jump when the player is touching the groud
        // prevents wall jumping as well
        if (!myFeetCollider.IsTouchingLayers(ground)) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            #region debugging code
            //if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground) == null)
            //    return;


            //GameObject groundObject =
            //    Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground).gameObject;
            //if (groundObject != null && groundObject.CompareTag("Ground"))
            //{
            //    rb.velocity += Vector2.up * jumpForce;
            //}
            #endregion

            rb.velocity += new Vector2(0f, jumpForce);
        }
    }

    private void Climb()
    {
        // prevents the player from showing the climb animation when not on the ladder.
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("ladder")))
        {
            anim.SetBool("isClimbing", false);

            rb.gravityScale = initialGravityScale;
            return;
        }


        float verticalSpeed =
                    CrossPlatformInputManager.GetAxis("Vertical") * climbSpeed;


        // physically move up
        rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
        rb.gravityScale = 0f;

        //anim.SetBool("isClimbing", Mathf.Abs(rb.velocity.y) > Mathf.Epsilon);

        anim.SetBool("isClimbing", true);

        // climbing animations
        if (Mathf.Abs(rb.velocity.y) < Mathf.Epsilon)
        {
            print("start playback");
            anim.StartPlayback();
        }
        else
        {
            print("stop playback");
            anim.StopPlayback();
        }

        // if the player is stationary on the ladder
        //if (Mathf.Abs(rb.velocity.y) < Mathf.Epsilon)
        //{
        //    anim.speed = 0;
        //    rb.gravityScale = 0f;
        //}
        //else
        //{
        //    set to my defaults
        //    anim.speed = 1f;
        //    rb.gravityScale = 10f;
        //}
    }

    private void Run()
    {
        //if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("ladder")))
        //{
        //    rb.velocity = new Vector2(0f, rb.velocity.y);
        //    return;
        //}

        float x_movement =
                    CrossPlatformInputManager.GetAxisRaw("Horizontal") * speed;

        rb.velocity = new Vector2(x_movement, rb.velocity.y);

        //if (Mathf.Abs(rb.velocity.x) > 0f)
        //{
        //    print(transform.position.x);
        //}

        anim.SetBool("isRunning", Mathf.Abs(rb.velocity.x) > 0f);
    }

    void FlipSprite()
    {
        bool movingHorizontally = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        // can't enter this statement if player is not in motion
        if (movingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);

            //(rb.velocity.x < 0f) ? new Vector2(-1f, 1f) : new Vector2(1f, 1f);
        }
    }
}


//if (CanClimb)
//{
//    if (CrossPlatformInputManager.GetButtonDown("Vertical"))
//    {
//        anim.SetBool("isClimbing", true);

//        float verticalSpeed =
//            CrossPlatformInputManager.GetAxisRaw("Vertical") * climbSpeed;

//        rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
//    }
//    else if (CrossPlatformInputManager.GetButtonUp("Vertical"))
//    {
//        anim.SetBool("isClimbing", false);
//    }
//}
//else
//{
//    anim.SetBool("isClimbing", false);
//}
