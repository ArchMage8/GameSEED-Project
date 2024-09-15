using UnityEngine;

public class NormalShooter : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float delayBetweenShots = 1f;
    [SerializeField] private int direction = 0;
    [SerializeField] private Animator animator;

    [Header("PlayerStuffs")]
    [SerializeField] private Transform Player;
    [SerializeField] private int minRange;
    private bool playerInRange = false;

    [Header("Audio Files")]
    public AudioClip SFXClip;

    private EnemyCombatHandler enemyCombatHandler;

    private float timeSinceLastShot = 0f;

    private void Start()
    {
        if (this.gameObject.GetComponent<EnemyCombatHandler>() != null)
        {
            enemyCombatHandler = GetComponent<EnemyCombatHandler>();
        }

        else
        {
            Debug.LogError("Combat Handler Missing");
        }
    }

    private void Update()
    {
        CheckDistance();
        
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= delayBetweenShots && enemyCombatHandler.CanAttack && playerInRange)
        {
            animator.SetTrigger("Shoot");
            ShootProjectile();
            timeSinceLastShot = 0f;
        }
    }

    private void ShootProjectile()
    {
        ShooterSFXManager.instance.PlaySFX(SFXClip);

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Transform child = projectile.transform.GetChild(0);
        if (child != null)
        {
            float angle = direction;
            child.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        Vector2 moveDirection = GetDirectionFromAngle(direction);
        projectile.AddComponent<ProjectileMover>().Initialize(moveDirection, speed);
    }

    private Vector2 GetDirectionFromAngle(int angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
    }

    private class ProjectileMover : MonoBehaviour
    {
        private Vector2 direction;
        private float speed;

        public void Initialize(Vector2 direction, float speed)
        {
            this.direction = direction;
            this.speed = speed;
        }

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }

    private void CheckDistance()
    {
        float distance = Vector2.Distance(transform.position, Player.position);

        if(distance <= minRange)
        {
            playerInRange = true;
        }

        else
        {
            playerInRange = false;
        }
    }
}
