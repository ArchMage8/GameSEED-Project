using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Spike Damage Settings")]
    public int damageAmount = 1;
    public float damageInterval = 1.0f;

    private bool isPlayerInRange = false;
    private HealthSystem playerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                isPlayerInRange = true;
                StartCoroutine(DealDamage());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            StopCoroutine(DealDamage());
        }
    }

    private IEnumerator DealDamage()
    {
        while (isPlayerInRange)
        {
            playerHealth.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
