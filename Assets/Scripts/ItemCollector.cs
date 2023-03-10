using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherriesCollected = 0;
    private Animator animator;

    [SerializeField] private AudioSource collectibleSound;
    [SerializeField] private Text cherriesText;

    private void Start()
    {
        cherriesCollected = PlayerPrefs.GetInt("cherriesCollected", 0);
        animator = GetComponent<Animator>();
        cherriesText.text = "Cherries: " + cherriesCollected;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collision.gameObject.GetComponent<Animator>().SetBool("isCollected", true);
            cherriesCollected++;
            cherriesText.text = "Cherries: " + cherriesCollected;
            collectibleSound.Play();
            //Debug.Log("Cherries Collected: " + cherriesCollected);
        }
    }

    public int GetCherryCount()
    {
        return cherriesCollected;
    }
}
