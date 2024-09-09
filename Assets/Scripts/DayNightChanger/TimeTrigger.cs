using UnityEngine;
using System.Collections;

public class TimeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Daycanvas;
    [SerializeField] private GameObject Nightcanvas;
    [SerializeField] private float animationStartWait = 2f;
    [SerializeField] private float animationEndWait = 2f;

    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)))
        {
            if (TimeCycle.Instance.TimeValue % 2 == 1)
            {
                StartCoroutine(ChangeTimeDay());
            }

            else
            {
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
       
        
        Checkpoint.instance.UpdateCheckpoint(transform);
        Daycanvas.SetActive(true);

        yield return new WaitForSeconds(animationStartWait);
        TimeCycle.Instance.IncreaseTimeValue();
        StartCoroutine(DisableCanvasDay());
    }

    private IEnumerator DisableCanvasDay()
    {
        yield return new WaitForSeconds(animationEndWait);
        Daycanvas.SetActive(false);
    } 
    
    private IEnumerator ChangeTimeNight()
    {
       
        
        Checkpoint.instance.UpdateCheckpoint(transform);
        Nightcanvas.SetActive(true);

        yield return new WaitForSeconds(animationStartWait);

        TimeCycle.Instance.IncreaseTimeValue();

      
     
        StartCoroutine(DisableCanvasNight());
    }

    private IEnumerator DisableCanvasNight()
    {
        yield return new WaitForSeconds(animationEndWait);
        Nightcanvas.SetActive(false);
    }
}
