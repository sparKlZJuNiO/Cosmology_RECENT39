using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class EnemyBulletScript : MonoBehaviour
{

    public GameObject player;
    public SpriteRenderer player2;
    private Rigidbody2D rb;
    public GameObject enemy;
    public bool death = false;
    public float force;
    public GameObject sourcey;
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;
    Animator anim;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GetComponent<SpriteRenderer>();
        sourcey = GameObject.FindGameObjectWithTag("audio source");
        enemy = GameObject.FindGameObjectWithTag("enemy");
        death = enemy.GetComponent<Health>().death;
        source = sourcey.GetComponent<AudioSource>();

        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force; // Controls the speed of the bullet

            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg; // Looks for a float y and x
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
    }




    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        sourcey = GameObject.FindGameObjectWithTag("audio source");

        enemy = GameObject.FindGameObjectWithTag("enemy");

        if (death == true)
        {
            Destroy(gameObject);
        }

        if (timer > 3)
        {
            Destroy(gameObject);
        }
    }

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("enemy"))
        {
            death = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("enemy"))
        {
            rb.tag = "Untagged";
        }
    }


            private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && death == false)
        {
            if (Input.GetMouseButton(0) == false)
            {
                other.gameObject.GetComponent<PlayerHealth>().currentHealth -= 20;
                player2.enabled = false;
                source.PlayOneShot(clip2);
                source.volume = 0.455f;
                Destroy(gameObject);
            }

        }

        if (other.gameObject.CompareTag("Player") && (Input.GetMouseButton(0)) && death == false)
        {
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(-direction.x, -direction.y).normalized * force; // Controls the speed of the bullet
            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg; // Looks for a float y and x
            transform.rotation = Quaternion.Euler(0, 0, rot);
            source.PlayOneShot(clip);
            source.volume = 0.350f;
            rb.GetComponent<BoxCollider2D>().isTrigger = false;
            // Debug.Log("Deflect");
        }
    }
}
