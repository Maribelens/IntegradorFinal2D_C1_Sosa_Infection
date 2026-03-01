using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip hoverSound;

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayHover()
    { 
        audioSource.PlayOneShot(hoverSound); 
    }
}
