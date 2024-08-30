using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage = 1;
    public float attackInterval = 2f;

    private bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                isColliding = true;
                StartCoroutine(DealDamageOverTime(playerHealth));
            }
        }
    }

    private IEnumerator DealDamageOverTime(HealthSystem playerHealth)
    {
        while (isColliding)
        {
            yield return new WaitForSeconds(attackInterval);
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}
