using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AudioSource deathSound;
    // Start is called before the first frame update
    private void Start()
    {
        animator= GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            PlaySound();
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("death");
        animator.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PlaySound()
    {
        deathSound.Play();
    }
}
