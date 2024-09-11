using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Health Pickup Settings")]
    public int healthIncrease;
    private bool hasBeenCollected = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenCollected)
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.GainHealth(healthIncrease);
                hasBeenCollected = true;
                Destroy(gameObject);
            }
        }
    }
}
