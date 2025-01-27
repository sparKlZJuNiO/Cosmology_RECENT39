using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting2 : MonoBehaviour
{
    private GameObject player;
    public GameObject bullet;
    public Transform bulletPos;
    public Transform bulletPos2;
    public Transform bulletPos3;
    private Rigidbody2D rb;
    public bool shooting = false;
    Animator anim;

    [SerializeField] private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);

        anim.SetBool("shooting", shooting);

        if (distance < 15 && rb.GetComponent<Health>().death == false)
        {
            timer += Time.deltaTime;

            if (timer > 0.8f)
            {
                timer = 0;
                shoot();
            }
        }
        else
        {
            shooting = false;
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        Instantiate(bullet, bulletPos2.position, Quaternion.identity);
        Instantiate(bullet, bulletPos3.position, Quaternion.identity);
        shooting = true;
    }
}
