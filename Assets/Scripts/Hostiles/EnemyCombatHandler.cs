using System.Collections;
using UnityEngine;

public class EnemyCombatHandler : MonoBehaviour
{
    [Header("Combat Settings")]
    public bool CanAttack;
    public bool CanMove;
    public float stunDuration;
    public float health;
    public bool isBlock;

    [Header("Boss Settings")]
    public bool isBoss;
    public GameObject sceneTrigger;

    [Header("Health Drop Settings")]
    public GameObject healthBoost; // Assign this in the inspector
    public int healthDropChance = 50; // 50% chance by default

    [Header("Flicker Settings")]
    public float flickerRate = 0.1f; // Flicker speed during stun
    public GameObject visual;

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
        if (!isBlock)
        {
            //Debug.Log("takeDamage");
            health -= damage;
            AudioRandomizer();
        }

        else if (isBlock)
        {
            if (PlayerStateManager.Instance.isSickleActive)
            {
                health -= damage;
                AudioRandomizer();
            }
        }

        if (health <= 0)
        {
            StartCoroutine(EnemyDeath());
        }
        else if( health > 0 || !isBoss)
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
            visual.SetActive(false);
            yield return new WaitForSeconds(flickerRate);
            visual.SetActive(true);
          
            timer += 1f;
        }
        visual.SetActive(true);
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
            visual.SetActive(false);
            yield return new WaitForSeconds(flickerRate);
            visual.SetActive(true); 
            timer += 1f;
        }
       

        if (isBoss)
        {
            sceneTrigger.SetActive(true);
        }

        Destroy(this.gameObject);
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
