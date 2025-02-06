using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement3 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public bool grounded = false;
    public bool walk = false;
    public bool shooting = false;
    [SerializeField] private SpriteRenderer enemy;
    private Vector2 UpdatePosition;
    public bool Ready = false;
    Animator anim;
    public int waypointIndex = 0;


    private Vector2 movement;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemy = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
                                                                     
  
        if (distance < 20 && rb.GetComponent<Health>().death == false && player.GetComponent<Movement>().bossfightStart == true)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            movement = direction;
            enemy.flipX = player.transform.position.x < this.transform.position.x; // Flips enemy
            Ready = true;
            // Debug.Log("Close");
            walk = false;
            shooting = true;
            //audioo.PlayOneShot(clip7);
           // audioo.volume = 0.105f;
            rb.GetComponent<EnemyShooting3>().shooting = true;
        }
        else if (distance > 20 && rb.GetComponent<Health>().death == false && player.GetComponent<Movement>().bossfightStart == true)
        {
            Ready = false;
            shooting = false;
            walk = true;
            rb.GetComponent<EnemyShooting3>().shooting = false;
           // Debug.Log("Distance");
        }

       if (rb.GetComponent<Health>().death == false && Ready == true&& player.GetComponent<Movement>().bossfightStart == true)
       {
            moveCharacter(movement);
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
        if (collision.gameObject.tag == ("stairs"))
        {
            rb.velocity = Vector2.up * 12;
        }
    }


    void moveCharacter(Vector2 direction)
    {
        if (Ready == true && rb.GetComponent<Health>().death == false && player.GetComponent<Movement>().bossfightStart == true) 
        {
            walk = false;
            shooting = true;
            anim.SetBool("walking", walk);
            anim.SetBool("shooting", shooting);
        }
    }
}
