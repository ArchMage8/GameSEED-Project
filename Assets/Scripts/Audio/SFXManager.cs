using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            //DontDestroyOnLoad(gameObject); // Keeps this object alive across scenes if needed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            volume = Mathf.Clamp01(volume);
            audioSource.PlayOneShot(clip);
        }
    }
}
