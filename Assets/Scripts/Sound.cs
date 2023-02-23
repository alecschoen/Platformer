using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    [SerializeField] private AudioSource[] fxSound;
    [SerializeField] private AudioSource music;

    // Update is called once per frame
    private void Start()
    {
        MuteSoundFx();
        MuteMusic();
    }
    
    
    private int IsFxMuted()
    {

        return PlayerPrefs.GetInt("isFxMuted", 0);

    }

    public void SwitchMuteFx()
    {
        if (PlayerPrefs.GetInt("isFxMuted", 0) == 0)
        {
            PlayerPrefs.SetInt("isFxMuted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isFxMuted", 0);
        }
        MuteSoundFx();
    }

    private void MuteSoundFx()
    {

        if (IsFxMuted() == 1)
        {
            for(int i = 0; i < fxSound.Length; i++)
            {
                fxSound[i].enabled = false;
            }
        }
        else if(IsFxMuted() == 0)
        {
            for (int i = 0; i < fxSound.Length; i++)
            {
                fxSound[i].enabled = true;
            }
        }

    }

    private int IsMusicMuted()
    {

        return PlayerPrefs.GetInt("isMusicMuted", 0);
    }

        private void MuteMusic()
    {
        if (IsMusicMuted() == 1)
        {
            music.enabled = false;
        }
        else if (IsMusicMuted() == 0)
        {
            music.enabled = true;
        }
    }

    public void SwitchMuteMusic()
    {
        if (PlayerPrefs.GetInt("isMusicMuted", 0) == 0)
        {
            PlayerPrefs.SetInt("isMusicMuted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isMusicMuted", 0);
        }
        MuteMusic();
    }

}
