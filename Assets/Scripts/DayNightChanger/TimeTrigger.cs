using UnityEngine;
using System.Collections;
using UnityEditor.Rendering;

public class TimeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Daycanvas;
    [SerializeField] private GameObject Nightcanvas;
    [SerializeField] private float animationStartWait = 2f;
    [SerializeField] private float animationEndWait = 2f;

    [Header("Audio Files")]
    public AudioClip SFXClipStart;
    public AudioClip SFXClipEnd;
    public float volume;

    private bool playerInRange;
    private bool inProgress = false;
    private HealthSystem playerHealth;
    private PlayerMovement playerMovement;

    private void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)) && !inProgress)
        {
            HealPlayer();
            playerMovement.StopMovement();

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
            playerHealth = collision.GetComponent<HealthSystem>();
            playerMovement = collision.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerHealth = null;
            playerMovement = null;
        }
    }

    private IEnumerator ChangeTimeDay()
    {
       TimeCycle.Instance.isChanging = true;
        
        Checkpoint.instance.UpdateCheckpoint(transform);
        Daycanvas.SetActive(true);

        SFXManager.instance.PlaySFX(SFXClipStart, volume);

        yield return new WaitForSeconds(animationStartWait);
        TimeCycle.Instance.IncreaseTimeValue();
        StartCoroutine(DisableCanvasDay());

        TimeCycle.Instance.isChanging = false;
        inProgress = false;
    }

    private IEnumerator DisableCanvasDay()
    {
        SFXManager.instance.PlaySFX(SFXClipEnd, volume);
        yield return new WaitForSeconds(animationEndWait);
        Daycanvas.SetActive(false);
    } 
    
    private IEnumerator ChangeTimeNight()
    {
        TimeCycle.Instance.isChanging = true;

        Checkpoint.instance.UpdateCheckpoint(transform);
        Nightcanvas.SetActive(true);

        SFXManager.instance.PlaySFX(SFXClipStart, volume);
        yield return new WaitForSeconds(animationStartWait);

        TimeCycle.Instance.IncreaseTimeValue();
        StartCoroutine(DisableCanvasNight());

        TimeCycle.Instance.isChanging = false;
        inProgress = false;
    }

    private IEnumerator DisableCanvasNight()
    {
        SFXManager.instance.PlaySFX(SFXClipEnd, volume);
        yield return new WaitForSeconds(animationEndWait);
        Nightcanvas.SetActive(false);
    }

    private void HealPlayer()
    {
        if (playerInRange)
        {
            playerHealth.GainHealth(4);
        }
    }
}
