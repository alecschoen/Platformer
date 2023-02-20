using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{

    [SerializeField] private string nextLevel;
    [SerializeField] private ParticleSystem confettiParticle;
    private bool isFinished = false;
    ItemCollector collector;
    PlayerMovement playerMovement;

    private void Start()
    {
        collector = GetComponent<ItemCollector>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End") && !isFinished)
        {
            Debug.Log("Finished");
            isFinished = true;
            confettiParticle.Play();
            PlayerPrefs.SetInt("cherriesCollected", collector.GetCherryCount());
            collision.gameObject.GetComponent<Animator>().SetBool("isFinished", true);
            Invoke("NextLevel", 3f);
            playerMovement.HasFinished();
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
