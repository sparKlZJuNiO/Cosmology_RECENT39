using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLevel : MonoBehaviour
{

    [SerializeField] public int SceneNumber;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("door"))
        {
            SceneNumber += 1;
            SceneManager.LoadScene(SceneNumber);
        }
    }
}
