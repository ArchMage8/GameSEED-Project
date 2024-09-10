using System.Collections;
using UnityEngine;

public class BossAttackTrack : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject target;
    public float delayBeforeAttack;
    public float moveSpeed;

    [HideInInspector] public bool isAttackComplete;

    public void TrackAttack()
    {
        StartCoroutine(TrackCoroutine());
    }

    private IEnumerator TrackCoroutine()
    {
        isAttackComplete = false;

        yield return new WaitForSeconds(delayBeforeAttack);

        Vector2 direction = target.transform.position.x > transform.position.x ? Vector2.right : Vector2.left;

        for (int i = 0; i < 4; i++)
        {
            GameObject projectile = Instantiate(objectToSpawn, transform.position + new Vector3(0, i, 0), Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * moveSpeed;
        }

        isAttackComplete = true;
    }
}
