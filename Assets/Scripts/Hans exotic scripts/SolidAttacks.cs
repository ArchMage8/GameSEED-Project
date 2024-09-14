using UnityEngine;

public class SolidAttacks : MonoBehaviour
{
    [Header("Stuff Settings")]
    [SerializeField] private int damageToPlayer = 1;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            Debug.Log("Took Damage");
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null){
                playerHealth.TakeDamage(damageToPlayer);
            }
        }
    }
}
