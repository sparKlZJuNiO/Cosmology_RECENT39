using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer enemy;
    private Vector2 UpdatePosition;
    private Vector2 movement;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
       // UpdatePosition = new Vector2(transform.position.x, transform.position.y); // Keeps the y-axis height
        //Debug.Log(distance);

        if (distance < 16 && rb.GetComponent<Health>().death == false)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
            enemy.flipX = player.transform.position.x < this.transform.position.x; // Flips enemy
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("enemy"))
        {
            rb.velocity = Vector2.up * 16;
        }
    }
            private void FixedUpdate()
    {
        if (rb.GetComponent<Health>().death == false)
        {
            moveCharacter(movement);
        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
        transform.position = new Vector2(transform.position.x, transform.position.y); // This would stick the player down the y-axis
    }
}
