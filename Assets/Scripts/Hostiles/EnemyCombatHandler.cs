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
    public SpriteRenderer visual;

    [Header("Audio Files")]
    public AudioClip SFXClip1;
    public AudioClip SFXClip2;
    public AudioClip SFXClip3;

    public float volume;

    private void Start()
    {
        if (isBoss)
        {
            sceneTrigger.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log("takeDamage");
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
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

        boxCollider2D.enabled = false;

        float timer = 0f;
        while (timer < stunDuration)
        {
            visual.enabled = !visual.enabled;
            yield return new WaitForSeconds(flickerRate);
            //visual.enabled = true;
            timer += 1f;
        }
        boxCollider2D.enabled = true; 
        //Debug.Log("Stun");
    }

    private IEnumerator EnemyDeath()
    {
        //Debug.Log("Death");
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        float timer = 0f;
        while (timer < stunDuration)
        {
            visual.enabled = !visual.enabled;
            yield return new WaitForSeconds(flickerRate);
            timer += 1f;
        }
       

        if (isBoss)
        {
            sceneTrigger.SetActive(true);
        }

        Destroy(gameObject);
    }

   

    private void DropHealth()
    {
        if (Random.Range(0, 100) < healthDropChance)
        {
            //Instantiate(healthBoost, transform.position, Quaternion.identity);
        }
    }

    private void AudioRandomizer()
    {
        int randomNumber = Random.Range(1, 4);

        if (randomNumber == 1)
        {
            SFXManager.instance.PlaySFX(SFXClip1, volume);
        }
        else if (randomNumber == 2)
        {
            SFXManager.instance.PlaySFX(SFXClip2,  volume);
        }
        else if (randomNumber == 3)
        {
            SFXManager.instance.PlaySFX(SFXClip3, volume);
        }
    }
}
