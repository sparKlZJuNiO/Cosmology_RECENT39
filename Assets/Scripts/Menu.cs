using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public float num = 25;
    public float num2 = 5;
    public float num3 = 1;
    public bool booom = false;
    [SerializeField] Animator transitionAnim;
    public bool booom2 = false;
    [SerializeField] GameObject Fade;
    [SerializeField] GameObject Fade2;
    [SerializeField] GameObject Fade3;
    [SerializeField] GameObject Fade4;
    public bool booom3 = false;
    public AudioSource source;
    public AudioClip clip;
    public AudioSource source2;
    public AudioClip clip2;



    private void Start()
    {
        num3 -= Time.deltaTime;
    }

    private void Update()
    {
        if (num3 < 1)
        {
            num3 -= Time.deltaTime;
            if (num3 < 0)
            {
                Fade.SetActive(false);
                num3 = -3;
            }
        }
        if (booom == true)
        {
            num -= Time.deltaTime;
            Fade.SetActive(true);
            transitionAnim.SetTrigger("End");
            source2.PlayOneShot(clip2);
            Fade2.SetActive(true);
            Fade3.SetActive(true);
            Fade4.SetActive(true);
            if (num < 1)
            {
                SceneManager.LoadScene(1);
            }
        }
        if (booom2 == true)
        {
            num2 -= Time.deltaTime;
            if (num2 < 2)
            {
                Application.Quit();
               // Debug.Log("Player Has Left The Game");
            }
        }
    }

    
    public void Play()
    {
        source.PlayOneShot(clip);
        booom = true;

    }

    public void Quit()
    {
        source.PlayOneShot(clip);
        booom2 = true;
        if (booom2 == true)
        {
            num2 -= Time.deltaTime;
        }
    }
}
