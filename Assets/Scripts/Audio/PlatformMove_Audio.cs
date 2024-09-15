using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove_Audio : MonoBehaviour
{
    [Header("Audio Files")]
    public AudioClip SFXClip;
    public AudioSource audioSource;

    private BlockMain blockMain;
    private bool canPlaySound = true;

    private void Start()
    {
        blockMain = GetComponent<BlockMain>();
    }

    private void Update()
    {
        if (blockMain.isMoving && canPlaySound)
        {
            StartCoroutine(PlayPlatformSound());
        }
    }

    private IEnumerator PlayPlatformSound()
    {
        canPlaySound = false;
        audioSource.PlayOneShot(SFXClip);
        yield return new WaitForSeconds(SFXClip.length);
        canPlaySound = true;
    }
}
