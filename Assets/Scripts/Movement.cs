using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;
    [SerializeField] private Light outdoorLight;
    [SerializeField] private float jumpCooldown = 1.08f;
    [SerializeField] private float jumpCooldown2 = 1.85f;
    [SerializeField] public float sprintingTime = 3.5f;
    public bool grounded = false;
    private bool isCooldown = false;
    public bool isCooldown4 = false;
    private bool moving = false;
    private bool boostAttack = false;
    private bool attacking = false;
    [SerializeField] private int jumps = 0;
   [SerializeField] private int maxJumps = 2;
    private bool isCooldown2 = false;
    private bool running = false;
    private bool isCooldown3 = false;
    private bool sprinting = false;
    private Vector2 UpdatePosition;
    [SerializeField] Transform plrObject;
    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer plr;
    [SerializeField] Color newColor1;
    [SerializeField] Color newColor2;
    private Vector3 target;
    [SerializeField] public float speedBoost = 182.3f;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        plr = GetComponent<SpriteRenderer>();
        target = transform.position;
        anim = GetComponent<Animator>();
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(jumpCooldown);
        isCooldown = false;
    }

    private IEnumerator Cooldown3()
    {
        isCooldown3 = true;
        yield return new WaitForSeconds(jumpCooldown2);
        isCooldown3 = false;
        jumps = 0;
    }

    private IEnumerator Cooldown2()
    {
        isCooldown2 = true;
        yield return new WaitForSeconds(3);
        isCooldown2 = false;
    }

    private IEnumerator Cooldown4()
    {
        isCooldown4 = false;
        yield return new WaitForSeconds(0.3f);
        isCooldown4 = true;
        yield return new WaitForSeconds(0.3f);
        isCooldown4 = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("isOnGround"))
        {
            grounded = true;
            UpdatePosition = new Vector2(transform.position.x, transform.position.y); // Keeps the y-axis height
           // Debug.Log("Works!");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("isOnGround"))
        {
            grounded = false;
            boostAttack = false;
            anim.SetBool("boostattacking", boostAttack);
            //Debug.Log("not!");
        }
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        speed = 5;

        running = false;
        sprinting = false;

        anim.SetBool("IsOnGround", grounded);
        anim.SetBool("running", running);
        anim.SetBool("sprinting", sprinting);
        anim.SetBool("boostattacking", boostAttack);

        if (running == false && sprinting == false)
        {
            moving = false;
            attacking = false;
            anim.SetBool("attacking", attacking);
            if (Input.GetMouseButton(0) && grounded == true)
            {
                boostAttack = true;
            }
            else if (Input.GetMouseButton(0) && grounded == false)
            {
                attacking = true;
                anim.SetBool("attacking", attacking);
            }
        }

        // Flipping player left and right
        if (horizontalInput > 0.01f)
        {
            plr.flipX = false;
            speed = 18;
            running = true;
            sprinting = false;
            anim.SetBool("running", running);

        }
       else if (horizontalInput < -0.01f)
       {
            plr.flipX = true;
            speed = 18;
            running = true;
            sprinting = false;
            anim.SetBool("running", running);
        }

       


        if (Input.GetKey(KeyCode.LeftShift))
        {
            {
                if (speed == 18 & grounded == true && sprintingTime > 1 && isCooldown2 == false)
                {
                    speed = 27;
                    rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
                    running = false;
                    sprinting = true;
                    anim.SetBool("sprinting", sprinting);
                    anim.SetBool("running", running);
                    anim.SetBool("IsOnGround", grounded);
                    //Debug.Log("Running");
                    jumpPower = 15.15f;
                    // Debug.Log(jumpPower);
                    sprintingTime -= Time.deltaTime; // Amount of time sprinting
                
                    if (sprintingTime < 1)
                    {
                        StartCoroutine(Cooldown2());
                        sprintingTime = 3.5f;
                        
                    }
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            jumpPower = 14.15f;
            running = false;
            sprinting = false;
            //Debug.Log("Walking");
        }

        if (Input.GetKey(KeyCode.Space) & !isCooldown & jumps < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            // Debug.Log("Jumping");
            grounded = false;
            attacking = false;
            boostAttack = false;
            jumps += 1;
            StartCoroutine(Cooldown());
            if (jumps == maxJumps)
            {
                StartCoroutine(Cooldown3());
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) // || is OR
        {
           // moving = true;
            boostAttack = false;
            attacking = false;
        }
            

        if (Input.GetMouseButton(0))
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) ) // || is OR
            {
                {

                    if (plr.flipX == false && isCooldown4 == false && grounded == true)
                    {
                        moving = true;
                        boostAttack = false;
                            transform.position = Vector2.MoveTowards(transform.position, target + transform.position, speedBoost * -Time.deltaTime); // Made by Jr (this was complicated, but done it myself to fix things)
                            transform.position = new Vector2(transform.position.x, UpdatePosition.y); // This would stick the player down the y-axis
                            attacking = true;
                            anim.SetBool("attacking", attacking);
                            // Debug.Log(-Time.deltaTime);
                            StartCoroutine(Cooldown4());
                    }
                    else if (plr.flipX == true && isCooldown4 == false && grounded == true)
                    {
                        moving = true;
                            transform.position = Vector2.MoveTowards(transform.position, target + transform.position, speedBoost * Time.deltaTime); // Made by Jr (this was complicated, but done it myself to fix things)
                            transform.position = new Vector2(transform.position.x, UpdatePosition.y); // This would stick the player down 
                            // Debug.Log(Time.deltaTime);
                            attacking = true;
                            anim.SetBool("attacking", attacking);
                           StartCoroutine(Cooldown4());
                        }
                    }
                }
        }
    }


