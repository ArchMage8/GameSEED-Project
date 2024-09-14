using UnityEngine;

public class ProjectileProperties : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private int damageToPlayer = 1;

    [Header("Audio Files")]
    public AudioClip SFXClip;

    //[SerializeField] private bool isBoss = false;

    private void Awake()
    {
        SFXManager.instance.PlaySFX(SFXClip);
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Test");
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
        }

    }
}
