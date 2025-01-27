using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public float followSpeed = 2f; // Follows target (Player)
    [SerializeField] public float yOffset = 1f; // Fixes height
    public Transform target; // Position of Player

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f); // Player's Position in X and Y, -10 is the normal position for z-axis 
        transform.position = Vector3.Slerp(transform.position,newPos, followSpeed * Time.deltaTime); // Slerp inserts between two vectors
    }
}
