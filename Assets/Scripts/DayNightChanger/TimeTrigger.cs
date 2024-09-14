using UnityEngine;
using System.Collections;

public class TimeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Daycanvas;
    [SerializeField] private GameObject Nightcanvas;
    [SerializeField] private float animationStartWait = 2f;
    [SerializeField] private float animationEndWait = 2f;

    [Header("Audio Files")]
    public AudioClip SFXClipStart;
    public AudioClip SFXClipEnd;
    

    private bool playerInRange;
    private bool inProgress = false;

    private void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)) && !inProgress)
        {
            if (TimeCycle.Instance.TimeValue % 2 == 1)
            {
                inProgress = true;
                StartCoroutine(ChangeTimeDay());
            }

            else
            {
                inProgress = true;
                StartCoroutine(ChangeTimeNight());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private IEnumerator ChangeTimeDay()
    {
       TimeCycle.Instance.isChanging = true;
        
        Checkpoint.instance.UpdateCheckpoint(transform);
        Daycanvas.SetActive(true);

        SFXManager.instance.PlaySFX(SFXClipStart);

        yield return new WaitForSeconds(animationStartWait);
        TimeCycle.Instance.IncreaseTimeValue();
        StartCoroutine(DisableCanvasDay());

        TimeCycle.Instance.isChanging = false;
        inProgress = false;
    }

    private IEnumerator DisableCanvasDay()
    {
        SFXManager.instance.PlaySFX(SFXClipEnd);
        yield return new WaitForSeconds(animationEndWait);
        Daycanvas.SetActive(false);
    } 
    
    private IEnumerator ChangeTimeNight()
    {
        TimeCycle.Instance.isChanging = true;

        Checkpoint.instance.UpdateCheckpoint(transform);
        Nightcanvas.SetActive(true);

        SFXManager.instance.PlaySFX(SFXClipStart);
        yield return new WaitForSeconds(animationStartWait);

        TimeCycle.Instance.IncreaseTimeValue();
        StartCoroutine(DisableCanvasNight());

        TimeCycle.Instance.isChanging = false;
        inProgress = false;
    }

    private IEnumerator DisableCanvasNight()
    {
        SFXManager.instance.PlaySFX(SFXClipEnd);
        yield return new WaitForSeconds(animationEndWait);
        Nightcanvas.SetActive(false);
    }
}
