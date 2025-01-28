using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    public Rigidbody2D rb;
    public bool grounded;
    private Animator anim;
    private bool walking = false;
    private Transform currentPoint;
    public float speed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("walking", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
            rb.velocity = new Vector2(rb.velocity.x, 1.5f);
            rb.gravityScale += 3.50f;
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform) // If our enemy reached current point then set for A
        {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform) // If our enemy reached current point then set for A
        {
            currentPoint = pointB.transform;
        }
    }
}
