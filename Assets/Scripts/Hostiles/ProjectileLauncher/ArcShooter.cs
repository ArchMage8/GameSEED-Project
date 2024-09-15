using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcShooter : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float launchSpeed = 10f;
    [SerializeField] private float launchAngle = 45f; // Angle in degrees
    [SerializeField] private float delayBetweenShots = 2f;
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
            LaunchProjectile();
            timeSinceLastShot = 0f;
        }
    }

    private void LaunchProjectile()
    {
        ShooterSFXManager.instance.PlaySFX(SFXClip);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float angleRad = launchAngle * Mathf.Deg2Rad;
            Vector2 launchDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
            Vector2 initialVelocity = launchDirection * launchSpeed;
            rb.velocity = initialVelocity;
        }
    }

    private void CheckDistance()
    {
        float distance = Vector2.Distance(transform.position, Player.position);

        if (distance <= minRange)
        {
            playerInRange = true;
        }

        else
        {
            playerInRange = false;
        }
    }
}
