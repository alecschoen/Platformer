using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{

    [SerializeField] private string nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            collision.gameObject.GetComponent<Animator>().SetBool("isFinished", true);
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(nextLevel).name);
    }
}
