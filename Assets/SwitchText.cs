using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchText : MonoBehaviour
{
    [SerializeField] private Text text;
    private bool isMuted = false;

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("isMuted", 0) == 0 && isMuted)
        {
            text.text = "Mute";
            isMuted = false;
        }
        else if(PlayerPrefs.GetInt("isMuted", 0) == 1 && !isMuted)
        {
            text.text = "Unmute";
            isMuted = true;
        }
    }
}
