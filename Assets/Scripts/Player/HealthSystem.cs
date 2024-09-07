using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header ("Health UI")]
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    [Space(20)]

    [Header ("Core System")]
    public int Health;
    public int invincibleDuration;
    [SerializeField] private float flickerRate = 0.5f;

    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    private bool Invincible;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Health > numOfHearts)
        {
            Health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!Invincible)
        {
            Health -= damage;
            StartCoroutine(InvincibleState());
            if (Health <= 0)
            {
                Health = 0;
                Death();
            }

            UpdateHearts();
        }
    }

    public void GainHealth(int amount)
    {
        Health += amount;
        if (Health > numOfHearts)
        {
            Health = numOfHearts;
        }

        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private IEnumerator InvincibleState()
    {
        Invincible = true;
        
        float timer = 0f;
        while (timer < invincibleDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flickerRate);
            timer += 0.1f;
        }

        Invincible = false;
        spriteRenderer.enabled = true;
        
    }

    private void Death()
    {
        playerMovement.StopMovement();
        Debug.Log("Setup the death logic");
    }
}
