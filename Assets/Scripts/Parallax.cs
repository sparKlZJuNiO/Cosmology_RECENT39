using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    GameObject player; // Reference to the player so we can track its position
    Renderer rend; // Reference to the renderer so we can modify its texture

    float playerStartPos; // Float used to track the starting position of the player
    public float speed = 0.5f; // How fast should we scroll? We change this for each layer

    void Start()
    {
        player = GameObject.Find("Player"); // Find the player
        rend = GetComponent<Renderer>(); // Find the renderer
        playerStartPos = player.transform.position.x; // Save our starting position
    }

    void Update()
    {
       float offset = (player.transform.position.x - playerStartPos) * speed; // How much to offset the texture by
       
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f)); // This sets our texture offset
    }
}
