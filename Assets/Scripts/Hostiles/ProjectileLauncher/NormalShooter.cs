using UnityEngine;

public class NormalShooter : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float delayBetweenShots = 1f;
    [SerializeField] private int direction = 0;

    private float timeSinceLastShot = 0f;

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= delayBetweenShots)
        {
            ShootProjectile();
            timeSinceLastShot = 0f;
        }
    }

    private void ShootProjectile()
    {
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
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
