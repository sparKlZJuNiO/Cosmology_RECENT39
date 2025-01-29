using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public bool grounded = false;
    [SerializeField] private SpriteRenderer enemy;
    private Vector2 UpdatePosition;
    [SerializeField] private Transform[] waypoints;
    public bool Ready = false;
    public bool waypointing = true;
    public int waypointIndex = 0;


    private Vector2 movement;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemy = GetComponent<SpriteRenderer>();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        UpdatePosition = new Vector2(transform.position.x, transform.position.y); // Keeps the y-axis height
        //Debug.Log(distance);


        if (distance < 16 && rb.GetComponent<Health>().death == false)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
            enemy.flipX = player.transform.position.x < this.transform.position.x; // Flips enemy
            Ready = true;
            waypointing = false;
        }
        else
        {
            Ready = false;
            waypointing = true;
        }

       if (rb.GetComponent<Health>().death == false && Ready == true && waypointing == false)
       {
            moveCharacter(movement);
      }
        else
        {
            Ready = false;
            waypointing = true;
       }

        // Move Enemy
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1 && waypointing == true && Ready == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("grounded"))
        {
            grounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("enemy"))
        {
            rb.velocity = Vector2.up * 16;
        }

        if (collision.gameObject.tag == ("grounded"))
        {
            grounded = true;
        }

        if (collision.gameObject.tag == ("waypoint") && waypointing == true)
        {
                waypointIndex += 1;
                enemy.flipX = false;
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        if (collision.gameObject.tag == ("waypoint2") && waypointing == true)
        {
            enemy.flipX = true;
            waypointIndex -= 1;
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }


    void moveCharacter(Vector2 direction)
    {
        if (Ready == true && waypointing == false)
        {
            rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
            transform.position = new Vector2(transform.position.x, transform.position.y); // This would stick the player down the y-axis
            waypointIndex = 0;
            moveSpeed = 5f;
        }
    }
}
