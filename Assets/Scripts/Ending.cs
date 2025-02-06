using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject title;
    public GameObject capsule;
    public AudioSource source;
    public AudioClip clip;
    public Text textyyy;
    public GameObject textObject;

    public float num = 9;

    private void Update()
    {
       num -= Time.deltaTime;
        if (num < 2)
        {
           title.SetActive(true);
           capsule.SetActive(false);
            source.PlayOneShot(clip);
            source.volume = 0.130f;
            if (num < -8)
            {
                textyyy.text = "PROGRAMMER: junior";
                if (num < -14)
                {
                    textyyy.text = "";
                }
                    if (num < -20)
                {
                    textyyy.text = "ARTIST: REMELL";
                }
                if (num < -23)
                {
                    textyyy.text = "";
                }
                if (num < -26)
                {
                    textyyy.text = "BOTH ARE COOL CREATORS YOU KNOW!";
                }
                if (num < -31)
                {
                    textyyy.text = "";
                }
                if (num < -35)
                {
                    textyyy.text = "WOW! THIS GAME IS COOL!";
                }
                if (num < -37)
                {
                    textyyy.text = "";
                }
                if (num < -39)
                {
                    textyyy.text = "TRY BEATING EACH LEVEL UNDER A CERTAIN TIME!";
                }
                if (num < -42)
                {
                    textyyy.text = "";
                }
                if (num < -46)
                {
                    textyyy.text = "OKAY..THIS WILL END NOW RIGHT?";
                }
                if (num < -48)
                {
                    textyyy.text = "System: THIS HAS ENDED!";
                }
            }
        }
            if (num < -50)
            {
                SceneManager.LoadScene(0);
            }
    }
}
