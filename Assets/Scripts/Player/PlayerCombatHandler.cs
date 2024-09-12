using System.Collections;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{
    [Header("Attack Settings")]
    public float rayLength = 5f;
    public int damageDealt = 1;

    [Header("Animation and Movement")]
    public Animator animator;
    public GameObject MainObject;

    private bool canAttack = true;
    public bool isAttacking { get; private set; }

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private void Start()
    {
        playerMovement = MainObject.GetComponent<PlayerMovement>();
        rb = MainObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canAttack && (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.J)))
        {
            StartCoroutine(Attack());
        }
        Debug.Log(isAttacking);
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        Debug.Log(isAttacking + "a");
        playerMovement.canMove = false;
        playerMovement.StopMovement();
        animator.SetTrigger("Attack");

        if (!playerMovement.isGrounded)
        {
            rb.gravityScale = 0.5f;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right * transform.localScale.x, rayLength);

        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Hostile"))
            {
                EnemyCombatHandler enemy = hit.collider.GetComponent<EnemyCombatHandler>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageDealt);
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
        if (playerMovement.isGrounded)
        
        {

            isAttacking = false;
            canAttack = true;
            playerMovement.canMove = true;
        }

        else
        {
            rb.gravityScale = 1;
            StartCoroutine(MidAirHandler());
        }
        
        
    }

    private IEnumerator MidAirHandler()
    {
        while (!playerMovement.isGrounded)
        {
            yield return null; 
        }
        canAttack = true;
        playerMovement.canMove = true;
    }
}
