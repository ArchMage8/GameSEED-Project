using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [Header("Detection")]
    public bool playerInRange = false;

    [Header("Audio Objects")]
    public GameObject OriginalBGM;
    public GameObject BossBGM;

    private void Update()
    {
        if (playerInRange == false)
        {
            OriginalBGM.SetActive(true);
            BossBGM.SetActive(false);
        }
        else
        {
            OriginalBGM.SetActive(false);
            BossBGM.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
