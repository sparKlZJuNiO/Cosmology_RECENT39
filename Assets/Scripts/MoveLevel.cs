using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLevel : MonoBehaviour
{

    [SerializeField] public int SceneNumber;
    [SerializeField] Animator transitionAnim;
    public float timer = 1;
    public bool checker = false;

    private void Update()
    {
        if (checker == true)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                SceneNumber += 1;
                SceneManager.LoadScene(SceneNumber);
                transitionAnim.SetTrigger("Start");
                checker = false;
                timer = 1;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("door"))
        {
            transitionAnim.SetTrigger("End");
            checker = true;
        }
    }
}
