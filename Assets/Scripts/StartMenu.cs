using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject StartMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    
    // Start is called before the first frame update
    private void Start()
    {
        StartMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        Debug.Log("Name:" + SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Settings() 
    { 
        StartMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        StartMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChooseLevel(int level)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 +level);
    }


}
