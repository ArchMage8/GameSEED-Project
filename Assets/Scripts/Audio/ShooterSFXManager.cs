using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterSFXManager : MonoBehaviour
{
    public static ShooterSFXManager instance;
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

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
