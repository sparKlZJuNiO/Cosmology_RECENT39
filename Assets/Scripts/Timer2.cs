using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer2 : MonoBehaviour
{
    public float currentTime;
    public float timeScale = 4;
    public float startingTime = 10f;
    private GameObject player;
    public GameObject gas;

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


        if (currentTime <= 40 && timeScale > 1)
        {
            gas.transform.localScale += new Vector3 (0.00018f,0,0); // Increase speed of the gas
            timeScale -= Time.deltaTime;
            if (timeScale < 1)
            {
                timeScale = 4;
            }
            if (currentTime <= 35 && timeScale > 1)
            {
                gas.transform.localScale += new Vector3(0.00025f, 0, 0);
                timeScale -= Time.deltaTime;
                if (timeScale < 1)
                {
                    timeScale = 4;
                }
                if (currentTime <= 30 && timeScale > 1)
                {
                    gas.transform.localScale += new Vector3(0.0037f, 0, 0);
                    timeScale -= Time.deltaTime;
                    if (timeScale < 1)
                    {
                        timeScale = 4;
                    }
                    if (currentTime <= 25 && timeScale > 1)
                    {
                        gas.transform.localScale += new Vector3(0.0045f, 0, 0);
                        timeScale -= Time.deltaTime;
                        if (timeScale < 1)
                        {
                            timeScale = 4;
                        }
                        if (currentTime <= 20 && timeScale > 1)
                        {
                            gas.transform.localScale += new Vector3(0.0058f, 0, 0);
                            timeScale -= Time.deltaTime;
                            if (timeScale < 1)
                            {
                                timeScale = 4;
                            }
                            if (currentTime <= 15 && timeScale > 1)
                            {
                                gas.transform.localScale += new Vector3(0.0018f, 0, 0);
                                timeScale -= Time.deltaTime;
                                if (timeScale < 1)
                                {
                                    timeScale = 4;
                                }
                                if (currentTime <= 10 && timeScale > 1)
                                {
                                    gas.transform.localScale += new Vector3(0.0022f, 0, 0);
                                    timeScale -= Time.deltaTime;
                                    if (timeScale < 1)
                                    {
                                        timeScale = 4;
                                    }
                                }
                                if (currentTime <= 0 && timeScale > 1)
                                {
                                    gas.transform.localScale += new Vector3(0.0026f, 0, 0);
                                    timeScale -= Time.deltaTime;
                                    if (timeScale < 1)
                                    {
                                        timeScale = 4;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

            if (currentTime <= 0)
        {
            currentTime = 0;
            player.GetComponent<PlayerHealth>().currentHealth = 0;
            // Your Code Here
        }
    }
}