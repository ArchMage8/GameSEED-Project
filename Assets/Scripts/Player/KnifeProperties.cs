using UnityEngine;
using System.Collections;

public class KnifeProperties : MonoBehaviour
{
    [Header("Knife Settings")]
    public float despawnTime = 5f;
    public int damage = 1;

    private bool isStuck = false;

    private void Start()
    {
        StartCoroutine(DespawnTimer(despawnTime));
    }

    private IEnumerator DespawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") && !isStuck)
        {
            StickToPlatform(collision);
        }
        else if (collision.CompareTag("Hostile"))
        {
            DealDamage(collision);
            Destroy(gameObject);
        }
    }

    private void StickToPlatform(Collider2D platform)
    {
        isStuck = true;
        transform.SetParent(platform.transform);
        StopAllCoroutines();
        StartCoroutine(DespawnTimer(despawnTime));
    }

    private void DealDamage(Collider2D enemyCollider)
    {
        EnemyCombatHandler enemy = enemyCollider.GetComponent<EnemyCombatHandler>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
