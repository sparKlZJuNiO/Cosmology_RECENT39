using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting3 : MonoBehaviour
{
    private GameObject player;
    public GameObject bullet;
    public Transform bulletPos;
    public Transform bulletPos2;
    private Rigidbody2D rb;
    public AudioSource source;
    public AudioClip clip;
    public bool shooting;
    public bool shooting2;
    Animator anim;

    [SerializeField] private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        shooting2 = rb.GetComponent<EnemyMovement3>().shooting;
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);



        if (distance < 18 && rb.GetComponent<Health>().death == false && shooting2 == false)
        {
            timer += Time.deltaTime;

            if (timer > 0.8f)
            {
                timer = 0;
                shoot();
            }
        }
        else if (rb.GetComponent<Health>().death == true)
        {
            shooting = false;
            anim.SetBool("shooting", shooting2);
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        Instantiate(bullet, bulletPos2.position, Quaternion.identity);
        shooting = true;
        source.PlayOneShot(clip);
        source.volume = 0.350f;
        anim.SetBool("shooting", shooting2);
    }
}
