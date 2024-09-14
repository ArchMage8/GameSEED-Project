using System.Collections;
using UnityEngine;

public class EnemyCombatHandler : MonoBehaviour
{
    [Header("Combat Settings")]
    public bool CanAttack;
    public bool CanMove;
    public float stunDuration;
    public float health;

    [Header("Boss Settings")]
    public bool isBoss;
    public GameObject sceneTrigger;

    [Header("Health Drop Settings")]
    public GameObject healthBoost; // Assign this in the inspector
    public int healthDropChance = 50; // 50% chance by default

    [Header("Flicker Settings")]
    public float flickerRate = 0.1f; // Flicker speed during stun
    private SpriteRenderer spriteRenderer;

    [Header("Audio Files")]
    public AudioClip SFXClip1;
    public AudioClip SFXClip2;
    public AudioClip SFXClip3;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isBoss)
        {
            sceneTrigger.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        AudioRandomizer();
        if (health <= 0)
        {
            StartCoroutine(EnemyDeath());
        }
        else
        {
            StartCoroutine(StunState());
        }
    }

    private IEnumerator StunState()
    {
        CanAttack = false;
        CanMove = false;

        float timer = 0f;
        while (timer < stunDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flickerRate);
            timer += flickerRate;
        }

        spriteRenderer.enabled = true;
        CanAttack = true;
        CanMove = true;
    }

    private IEnumerator EnemyDeath()
    {
        float blinkDuration = 0f;
        float blinkInterval = 0.1f;
        Color originalColor = spriteRenderer.color;
        Color blinkColor = Color.red;

        // Blink red before destruction
        while (blinkDuration < stunDuration) // X represents the duration in seconds
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
            blinkDuration += blinkInterval * 2;
        }

        // Drop health if not a boss
        if (!isBoss)
        {
            DropHealth();
        }

        // If boss, activate scene trigger
        else if (isBoss)
        {

            sceneTrigger.SetActive(true);

            Destroy(gameObject);
        }
    }

    private void DropHealth()
    {
        if (Random.Range(0, 100) < healthDropChance)
        {
            Instantiate(healthBoost, transform.position, Quaternion.identity);
        }
    }

    private void AudioRandomizer()
    {
        int randomNumber = Random.Range(1, 4);

        if (randomNumber == 1)
        {
            SFXManager.instance.PlaySFX(SFXClip1);
        }
        else if (randomNumber == 2)
        {
            SFXManager.instance.PlaySFX(SFXClip2);
        }
        else if (randomNumber == 3)
        {
            SFXManager.instance.PlaySFX(SFXClip3);
        }
    }
}
