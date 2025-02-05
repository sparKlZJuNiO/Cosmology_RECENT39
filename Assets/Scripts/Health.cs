using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Health : MonoBehaviour
{
    [SerializeField] public int health = 1;

    //private int MAX_HEALTH = 1;


    [SerializeField] public bool death = false;

    private bool grounded = false;

    private GameObject player;

    private Rigidbody2D rb;

    public AudioSource source;
    public AudioClip clip;

    Animator anim;

    [SerializeField]  private bool Touched = false;

    // [SerializeField] private int howManyTimesAway;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            Touched = true; // How many times has the player collided with the enemy
            rb.velocity = Vector2.up * 6;
        }

        if (collision.gameObject.tag == ("door"))
        {
            Destroy(gameObject);
        }

         if (collision.gameObject.tag == ("bullet"))
            {
            Damage(7);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        { 
        Touched = false;
    }
       // if (howManyTimesAway <= 10)
       // {
          //howManyTimesAway += 1; // Healing is based on how many times has the player left sight of the enemy
          //Heal(3 * howManyTimesAway); // Made by JR // This can be used to make harder enemies in the game
       // }
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) & Touched == true && player.GetComponent<Movement>().isCooldown4 == false)
        {
            Damage(3);
        }
    }
       

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }

        this.health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    /*
    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }
    */

    public void Die()
    {
        if (death == false) 
        {
            //Debug.Log("I am dead");
            death = true;
            anim.SetBool("death", death);
            rb.tag = "Untagged";
            source.PlayOneShot(clip);
            source.volume = 0.650f;
            rb.GetComponent<BoxCollider2D>().offset = new Vector2(-0.1860085f, -0.7303029f);
            rb.GetComponent<BoxCollider2D>().size = new Vector2(2.807747f, 0.9384806f);
            rb.GetComponent<BoxCollider2D>().isTrigger = true;
            rb.GetComponent<Rigidbody2D>().isKinematic = true;
            rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            rb.mass = 83.15f;
            rb.gravityScale = 0;
            rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y - 0.5f);
        }
    }
}
