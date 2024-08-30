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
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= delayBetweenShots && enemyCombatHandler.CanAttack)
        {
            LaunchProjectile();
            timeSinceLastShot = 0f;
        }
    }

    private void LaunchProjectile()
    {
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
}
