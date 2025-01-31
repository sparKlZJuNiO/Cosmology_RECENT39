using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int currentHealth;
    private GameObject player;
    public GameObject gameOverUI;
    public GameObject fade;

    [HideInInspector] public bool playerIsDead;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart; 
    [SerializeField] private Image[] heartContainers;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (currentHealth <= 0 && !playerIsDead)
         {
            GameOver();
         }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
     }

    private void OnGUI()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (currentHealth < 100)
            {
                heartContainers[3].fillAmount = 0;
                if (currentHealth < 80)
                {
                    heartContainers[2].fillAmount = 0;
                    if (currentHealth < 60)
                    {
                        heartContainers[1].fillAmount = 0;
                        if (currentHealth < 40)
                        {
                            heartContainers[0].fillAmount = 0;
                            currentHealth = 0;
                        }
                    }
                }
            }
        }
    }

    private void GameOver()
    {
        playerIsDead = true;
        gameOverUI.SetActive(true);
        gameObject.SetActive(false);
        fade.SetActive(false);

    }

    public void Restart()
    {

        SceneManager.LoadScene(player.GetComponent<MoveLevel>().SceneNumber);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        OnGUI();
    }
}
