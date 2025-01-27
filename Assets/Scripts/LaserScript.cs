using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    [SerializeField] public GameObject plr;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            plr.GetComponent<PlayerHealth>().currentHealth = 0;
        }
    }

   
}
