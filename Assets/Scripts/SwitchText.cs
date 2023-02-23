using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchText : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private string afterText;

    // Update is called once per frame
    private void Start()
    {
        SetText();
    }
    public void SetText()
    {
        if (PlayerPrefs.GetInt("is" + afterText + "Muted", 0) == 0)
        {
            text.text = "Mute " + afterText;
        }
        else if (PlayerPrefs.GetInt("is" + afterText + "Muted", 0) == 1)
        {
            text.text = "Unmute " + afterText;
        }
    }
}
