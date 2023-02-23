using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject levelSelectMenuUI;
    [SerializeField] private GameObject characterSelectMenuUI;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject backButton;
    [SerializeField] private Text title;

    // Start is called before the first frame update
    private void Start()
    {
        startMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(false);
        characterSelectMenuUI.SetActive(false);
        backButton.SetActive(false);
        title.text = "Platform One";
        //Debug.Log("Name:" + SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        startMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        backButton.SetActive(true);
        title.text = "Settings";
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        startMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(false);
        characterSelectMenuUI.SetActive(false);
        backButton.SetActive(false);
    }

    public void LevelSelect()
    {
        levelSelectMenuUI.SetActive(true);
        startMenuUI.SetActive(false);
        backButton.SetActive(true);
        title.text = "Select Level";
    }

    public void CharacterSelect()
    {
        characterSelectMenuUI.SetActive(true);
        startMenuUI.SetActive(false);
        backButton.SetActive(true);
        title.text = "Select Character";
    }

    public void ChooseLevel(int level)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }


}
