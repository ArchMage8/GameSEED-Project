using System.Collections;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{
    public float rayLength = 5f;
    public int damageX = 1;
    public Animator playerAnimator;

    private bool canAttack = true;
    public bool isAttacking { get; private set; }

    private void Update()
    {
        if (canAttack && (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.J)))
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;
        playerAnimator.SetTrigger("Attack");

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right * transform.localScale.x, rayLength);

        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Hostile"))
            {
                EnemyCombatHandler enemy = hit.collider.GetComponent<EnemyCombatHandler>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageX);
                }
            }
        }

        yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
        canAttack = true;
    }
}
