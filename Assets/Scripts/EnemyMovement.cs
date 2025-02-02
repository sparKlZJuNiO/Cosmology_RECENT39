using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public bool grounded = false;
    public bool walk = false;
    public bool shooting = false;
    [SerializeField] private SpriteRenderer enemy;
    private Vector2 UpdatePosition;
    [SerializeField] public GameObject[] waypoints;
    public bool Ready = false;
    public bool waypointing = true;
    Animator anim;
    public int waypointIndex = 0;


    private Vector2 movement;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemy = GetComponent<SpriteRenderer>();
        transform.position = waypoints[waypointIndex].transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 20 && rb.GetComponent<Health>().death == false)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            movement = direction;
            enemy.flipX = player.transform.position.x < this.transform.position.x; // Flips enemy
            Ready = true;
            waypointing = false;
            // Debug.Log("Close");
            walk = false;
            shooting = true;
            rb.GetComponent<EnemyShooting>().shooting = true;
        }
        else if (distance > 20 && rb.GetComponent<Health>().death == false)
        {
            Ready = false;
            shooting = false;
            waypointing = true;
            walk = true;
            rb.GetComponent<EnemyShooting>().shooting = false;
           // Debug.Log("Distance");
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
        if (waypointIndex <= waypoints.Length - 1 && waypointing == true && Ready == false && rb.GetComponent<Health>().death == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            moveSpeed = 5f;
            walk = true;
            shooting = false;
            anim.SetBool("walking", walk);
            anim.SetBool("shooting", shooting);
            waypoints[0].GetComponent<BoxCollider2D>().isTrigger = false;
            waypoints[1].GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("grounded"))
        {
            grounded = false;
            anim.SetBool("grounded", grounded);
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
            anim.SetBool("grounded", grounded);
        }

        if (collision.gameObject.tag == ("waypoint") && waypointing == true)
        {
                waypointIndex = 1;
                enemy.flipX = false;
        }
       
        if (collision.gameObject.tag == ("waypoint2") && waypointing == true)
        {
            enemy.flipX = true;
            waypointIndex = 0;
        }
        if (collision.gameObject.tag == ("stairs"))
        {
            rb.velocity = Vector2.up * 12;
        }
    }


    void moveCharacter(Vector2 direction)
    {
        if (Ready == true && waypointing == false && rb.GetComponent<Health>().death == false) 
        {
            waypointIndex = 1;
            waypoints[0].GetComponent<BoxCollider2D>().isTrigger = true;
            waypoints[1].GetComponent<BoxCollider2D>().isTrigger = true;
            walk = false;
            shooting = true;
            anim.SetBool("walking", walk);
            anim.SetBool("shooting", shooting);
        }
    }
}
