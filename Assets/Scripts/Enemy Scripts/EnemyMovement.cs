using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    [SerializeField] Transform[] wayPoints = null;

    private int index = 0;
    private Rigidbody2D rb;
    private Collider2D col;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // cycle thru waypoints
        Vector2 currentPos = transform.position;
        Vector2 targetPos = wayPoints[index % wayPoints.Length].position;

        // enemy will move left
        if (currentPos.x > targetPos.x
            && Vector2.Distance(currentPos, targetPos) > .5f)
        {
            rb.velocity = Vector2.left * moveSpeed;
            FlipEnemySprite();
        }
        // enemy will move right
        else if (currentPos.x < targetPos.x && Vector2.Distance(currentPos, targetPos) > .5f)
        {
            rb.velocity = Vector2.right * moveSpeed;
            FlipEnemySprite();
        }
        // enemy has reached the waypoint
        else
        {
            ++index;
        }

    }

    // makes sure sprite image/animation and movement match
    private void FlipEnemySprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
    }
}
