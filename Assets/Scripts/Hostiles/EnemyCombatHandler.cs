using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCombatHandler : MonoBehaviour
{
    [Header("Gameplay System: ")]
    public bool CanAttack;
    public bool CanMove;
    [SerializeField] private int health;
    public float stunDuration;
    [Space(25)]

    [Header("Visual: ")]
    [SerializeField] private float flickerRate = 0.5f;

    
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(StunState());
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
            timer += 0.1f;
        }

        spriteRenderer.enabled = true;
        CanAttack = true;
        CanMove = true;
    }
}
