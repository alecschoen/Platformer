using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private bool isMuted = false;

    [SerializeField] private AudioSource[] audioSources;

    // Update is called once per frame
    private void Update()
    {
       MuteSound();
    }
    
    
    private int IsMuted()
    {

        return PlayerPrefs.GetInt("isMuted", 0);

    }

    public void SwitchMute()
    {
        if (PlayerPrefs.GetInt("isMuted", 0) == 0)
        {
            PlayerPrefs.SetInt("isMuted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isMuted", 0);
        }
    }

    private void MuteSound()
    {

        if (IsMuted() == 1 && !isMuted)
        {
            for(int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].enabled = false;
            }
            isMuted = true;
        }
        else if(IsMuted() == 0 && isMuted)
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].enabled = true;
            }

            isMuted = false;
        }

    }

    
}
