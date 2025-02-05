using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public float num = 25;
    public float num2 = 5;
    public float num3 = 1;
    public bool booom = false;
    [SerializeField] Animator transitionAnim;
    [SerializeField] Animator transitionAnim2;
    public bool booom2 = false;
    [SerializeField] GameObject Fade;
    [SerializeField] GameObject Fade2;
    [SerializeField] GameObject Fade3;
    [SerializeField] GameObject Fade4;
    [SerializeField] Text textyyy;
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
            if (num <= 12)
            {
                transitionAnim2.SetTrigger("End");
                textyyy.text = "Ignition sequence start";
            }
            if (num <= 9)
            {
                transitionAnim2.SetTrigger("Start");
                textyyy.text = "6";
            }
            if (num <= 8)
            {
                transitionAnim2.SetTrigger("Start");
                textyyy.text = "5";
            }
            if (num <= 7)
            {
                transitionAnim2.SetTrigger("Start");
                textyyy.text = "4";
            }
            if (num <= 6)
            {
                transitionAnim2.SetTrigger("Start");
                textyyy.text = "4";
            }
            if (num <= 5)
            {
                transitionAnim2.SetTrigger("Start");
                textyyy.text = "3";
            }
            if (num <= 4)
            {
                transitionAnim2.SetTrigger("Start");
                textyyy.text = "2";
            }
            if (num <= 3)
            {
                transitionAnim2.SetTrigger("Start");
                textyyy.text = "1";
            }
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
