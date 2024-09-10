using System.Collections;
using UnityEngine;

public class BossAttackStar : MonoBehaviour
{
    public GameObject objectToShoot;
    public int maxRounds;
    public float shootDelay;
    public float moveSpeed;

    [HideInInspector] public bool isAttackComplete;

    public void StarAttack()
    {
        StartCoroutine(StarCoroutine());
    }

    private IEnumerator StarCoroutine()
    {
        isAttackComplete = false;
        int rounds = 0;

        while (rounds < maxRounds)
        {
            ShootIn8Directions();
            rounds++;
            yield return new WaitForSeconds(shootDelay);
        }

        isAttackComplete = true;
    }

    private void ShootIn8Directions()
    {
        Vector2[] directions = {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right,
            new Vector2(1, 1).normalized, new Vector2(-1, 1).normalized,
            new Vector2(1, -1).normalized, new Vector2(-1, -1).normalized
        };

        foreach (Vector2 dir in directions)
        {
            GameObject projectile = Instantiate(objectToShoot, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = dir * moveSpeed;
        }
    }
}
