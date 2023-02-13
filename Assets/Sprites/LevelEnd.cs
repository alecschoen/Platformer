using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            collision.gameObject.GetComponent<Animator>().SetBool("isFinished", true);
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("Level 2").name);
    }
}
