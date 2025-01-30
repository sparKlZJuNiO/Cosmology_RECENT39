using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LungeManager : MonoBehaviour
{
    public Image lungeBar;
    public float lungeAmount = 100f;
    private bool isCooldown = false;
    private GameObject player;
    private float cooldownTime = 1.67f;

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            if (player.GetComponent<Movement>().grounded == true && player.GetComponent<Movement>().isCooldown4 == false)
            {
                TakeLunge(3);
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) 
        {
            Increase(5);
        }
    }

    public void TakeLunge(float amount)
    {
        lungeAmount -= amount;
        lungeBar.fillAmount = lungeAmount / 100f;
        StartCoroutine(Cooldown());
    }

    public void Increase(float increaseAmount)
    {
        lungeAmount += increaseAmount;
        lungeAmount = Mathf.Clamp(lungeAmount, 0, 100);
        lungeBar.fillAmount = lungeAmount / 100f;

    }
}
