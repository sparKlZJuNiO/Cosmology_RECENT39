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
        }

<<<<<<< Updated upstream
        if (collision.gameObject.tag == ("bullet"))
        {
            //Damage(10);
            Debug.Log("Here");
        }

        if (collision.gameObject.tag == ("door"))
        {
            Destroy(gameObject);
        }
=======
         if (collision.gameObject.tag == ("bullet"))
            {
            Die();
            }
>>>>>>> Stashed changes
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
        if (Input.GetMouseButtonDown(0) & Touched == true)
        {
            Damage(10);
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
        //Debug.Log("I am dead");
        death = true;
        anim.SetBool("death", death);
        rb.tag = "Untagged";
    }
}
