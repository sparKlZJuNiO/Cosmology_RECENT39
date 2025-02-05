using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

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
    public GameObject Hammer_2D;
    public bool attacking = false;
    [SerializeField] private int jumps = 0;
   [SerializeField] private int maxJumps = 2;
    public bool gasIn;
    public bool timey;
    public GameObject cameraoff;
    private bool isCooldown2 = false;
    private bool running = false;
    private bool isCooldown3 = false;
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip7;
    public AudioClip clip8;
    [SerializeField] GameObject [] boxes;
    public AudioClip clip5;
    public AudioClip clip6;
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
        boxes = GameObject.FindGameObjectsWithTag("event");
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
        if (collision.gameObject.tag == ("isOnGround") || (collision.gameObject.tag == ("stairs") || (collision.gameObject.tag == ("height") || (collision.gameObject.tag == ("touchEvent")))))
        {
            grounded = true;
            UpdatePosition = new Vector2(transform.position.x, transform.position.y); // Keeps the y-axis height
            source.PlayOneShot(clip4);
            cameraoff.GetComponent<CameraFollow>().yOffset = 5.52f;
            // Debug.Log("Works!");
        }

        if (collision.gameObject.tag == ("height"))
        {
            cameraoff.GetComponent<CameraFollow>().yOffset = -2.48f;
        }

        if (collision.gameObject.tag == ("laser"))
        {
            source.PlayOneShot(clip6);
            source.volume = 0.400f;
        }

        if (collision.gameObject.tag == ("waypoint"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        if (collision.gameObject.tag == ("touchEvent"))
        {
            boxes[0].AddComponent<Rigidbody2D>();
            boxes[1].AddComponent<Rigidbody2D>();
            boxes[2].AddComponent<Rigidbody2D>();
            boxes[3].AddComponent<Rigidbody2D>();
            boxes[4].AddComponent<Rigidbody2D>();
            timey = true;
            Time.timeScale = 0.1f;
            source.PlayOneShot(clip7);
            rb.GetComponent<Rigidbody2D>().gravityScale = 191.34f;
        }
        if (collision.gameObject.tag == ("waypoint2"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        if (collision.gameObject.tag == ("event"))
        {
            plr.GetComponent<PlayerHealth>().currentHealth = 0;
            boxes[0].GetComponent<Rigidbody2D>().mass = 135.31f;
            boxes[1].GetComponent<Rigidbody2D>().mass = 135.31f;
            boxes[2].GetComponent<Rigidbody2D>().mass = 135.31f;
            boxes[3].GetComponent<Rigidbody2D>().mass = 135.31f;
            boxes[4].GetComponent<Rigidbody2D>().mass = 135.31f;
            Time.timeScale = 1f;
            source.PlayOneShot(clip8);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("gas"))
        {
            gasIn = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("gas"))
        {
            gasIn = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("isOnGround") || (collision.gameObject.tag == ("stairs") || (collision.gameObject.tag == ("height") || (collision.gameObject.tag == ("touchEvent")))))
        {
            boostAttack = false;
            anim.SetBool("boostattacking", boostAttack);
            //Debug.Log("not!");
        }

        if (collision.gameObject.tag == ("waypoint"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        if (collision.gameObject.tag == ("waypoint2"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        if (collision.gameObject.tag == ("touchEvent"))
        {
            boxes[0].AddComponent<Rigidbody2D>();
            boxes[1].AddComponent<Rigidbody2D>();
            boxes[2].AddComponent<Rigidbody2D>();
            boxes[3].AddComponent<Rigidbody2D>();
            boxes[4].AddComponent<Rigidbody2D>();
            collision.gameObject.tag = "isOnGround";
            source.PlayOneShot(clip8);
            boxes[0].GetComponent<Rigidbody2D>().tag = "isOnGround";
            boxes[1].GetComponent<Rigidbody2D>().tag = "isOnGround";
            boxes[2].GetComponent<Rigidbody2D>().tag = "isOnGround";
            boxes[3].GetComponent<Rigidbody2D>().tag = "isOnGround";
            boxes[4].GetComponent<Rigidbody2D>().tag = "isOnGround";
            rb.GetComponent<Rigidbody2D>().gravityScale = 3.49f;
        }
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        speed = 5;

        if (timey == true && Time.timeScale < 1)
        {
            Time.timeScale += Time.deltaTime;
        }

        running = false;
        sprinting = false;

        anim.SetBool("IsOnGround", grounded);
        anim.SetBool("running", running);
        anim.SetBool("sprinting", sprinting);
        anim.SetBool("boostattacking", boostAttack);

        if (running == false && sprinting == false && attacking == false)
        {
            moving = false;
            attacking = false;
            anim.SetBool("attacking", attacking);
            if (Input.GetMouseButton(0) && grounded == true && running == false && sprinting == false)
            {
                boostAttack = true;
                Hammer_2D.GetComponent<Light2D>().enabled = true;

            }
            else if (Input.GetMouseButton(0) && grounded == false )
            {
                attacking = true;
                boostAttack = false;
                anim.SetBool("attacking", attacking);
                Hammer_2D.GetComponent<Light2D>().enabled = false;
            }
        }

        if (gasIn == true)
        {
            rb.GetComponent<PlayerHealth>().currentHealth -= Time.deltaTime * 20;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            {
            source.PlayOneShot(clip);
            //source.volume = 0.100f;
            Hammer_2D.GetComponent<Light2D>().enabled = false;
        }
               

        // Flipping player left and right
        if (horizontalInput > 0.01f)
        {
            plr.flipX = false;
            speed = 18;
            running = true;
            attacking = false;
            boostAttack = false;
            sprinting = false;
            //source.panStereo = 0.664f;
            anim.SetBool("running", running);
                 anim.SetBool("attacking", attacking);
            anim.SetBool("boostattacking", boostAttack);
            Hammer_2D.GetComponent<Light2D>().enabled = false;
        }
       else if (horizontalInput < -0.01f)
       {
            plr.flipX = true;
            speed = 18;
            attacking = false;
            boostAttack = false;
            running = true;
            sprinting = false;
          //  source.panStereo = -0.664f;
            anim.SetBool("running", running);
            anim.SetBool("attacking", attacking);
            anim.SetBool("boostattacking", boostAttack);
            Hammer_2D.GetComponent<Light2D>().enabled = false;
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
                    Hammer_2D.GetComponent<Light2D>().enabled = false;
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
            if (source.volume <= 0.800)
            {
                source.volume += Time.deltaTime;
            } 
        }
            

        if (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
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
                       // source.volume = 0.15f;
                        source.panStereo = 0.664f;
                        Hammer_2D.GetComponent<Light2D>().enabled = false;
                        source.PlayOneShot(clip2);
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
                       // source.volume = 0.13f;
                        Hammer_2D.GetComponent<Light2D>().enabled = false;
                        source.panStereo = -0.664f;
                        source.PlayOneShot(clip3);
                        StartCoroutine(Cooldown4());
                        }
                    }
                }
        }
    }


