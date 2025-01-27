using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime = 10f;
    private GameObject player;

    [SerializeField] Text countdownText;
    void Start()
    {
        currentTime = startingTime;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("Timer : 0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            player.GetComponent<PlayerHealth>().currentHealth = 0;
            // Your Code Here
        }
    }
}