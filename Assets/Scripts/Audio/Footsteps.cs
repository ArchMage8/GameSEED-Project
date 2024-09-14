using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("Footstep Settings")]
    public AudioClip footstepClip;
    public AudioSource audioSource;
    public bool isWalking = false;  // The bool that controls whether sound plays

    private PlayerMovement playerMovement;
    private bool canPlaySound = true;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        isWalking = playerMovement.isMoving;

        if (isWalking && canPlaySound && playerMovement.isGrounded)
        {
            StartCoroutine(PlayFootstepSound());
        }
    }

    private IEnumerator PlayFootstepSound()
    {
        canPlaySound = false;
        audioSource.PlayOneShot(footstepClip);
        yield return new WaitForSeconds(footstepClip.length);  // Ensures 0.5s delay (or the clip length)
        canPlaySound = true;
    }
}
