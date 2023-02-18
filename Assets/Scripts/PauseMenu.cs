using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        PlayerPrefs.SetInt("cherriesCollected", 0);
        Application.Quit();
    }

    public void ReturnToStartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start Screen");
    }

}
