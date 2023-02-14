using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{

    [SerializeField] private string nextLevel;
    private bool isFinished = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End") && !isFinished)
        {
            Debug.Log("Finished");
            isFinished = true;
            collision.gameObject.GetComponent<Animator>().SetBool("isFinished", true);
            Invoke("NextLevel", 5);
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
