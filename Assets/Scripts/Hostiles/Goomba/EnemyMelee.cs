using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage = 1;
    public float attackInterval = 2f;

    private bool isColliding = false;
    private bool canAttack = true;

    private HealthSystem playerHealth;

    private void Update()
    {
        if(canAttack && isColliding)
        {
            //Debug.Log("Test");
            playerHealth.TakeDamage(damage);
            StartCoroutine(DealDamageOverTime());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = true;
            playerHealth = collision.GetComponent<HealthSystem>();
           
            
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
             playerHealth = null;
            isColliding = false;
           
        }
    }
}
