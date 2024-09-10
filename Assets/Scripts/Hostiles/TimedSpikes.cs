using System.Collections;
using UnityEngine;

public class TimedSpikes : MonoBehaviour
{
    [Header("Spike Movement Settings")]
    public GameObject spikeObject;
    public float spikeMoveUpAmount = 2f;
    public float spikeEnableDelay = 1.0f;
    public float spikeDisableDelay = 2.0f;

    private Vector3 originalPosition;
    private bool isPlayerInRange = false;
    private Coroutine spikeCoroutine;

    private void Start()
    {
        originalPosition = spikeObject.transform.localPosition;
        spikeObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerInRange)
        {
            isPlayerInRange = true;
            if (spikeCoroutine != null)
            {
                StopCoroutine(spikeCoroutine);
            }
            spikeCoroutine = StartCoroutine(EnableSpikeAfterDelay());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isPlayerInRange)
        {
            isPlayerInRange = false;
            if (spikeCoroutine != null)
            {
                StopCoroutine(spikeCoroutine);
            }
            spikeCoroutine = StartCoroutine(DisableSpikeAfterDelay());
        }
    }

    private IEnumerator EnableSpikeAfterDelay()
    {
        yield return new WaitForSeconds(spikeEnableDelay);
        spikeObject.SetActive(true);
        spikeObject.transform.localPosition = originalPosition + new Vector3(0f, spikeMoveUpAmount, 0f);
    }

    private IEnumerator DisableSpikeAfterDelay()
    {
        yield return new WaitForSeconds(spikeDisableDelay);
        spikeObject.transform.localPosition = originalPosition;
        spikeObject.SetActive(false);
    }
}
