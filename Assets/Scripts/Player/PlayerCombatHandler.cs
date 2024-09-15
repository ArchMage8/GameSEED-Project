using System.Collections;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{
    [Header("Attack Settings")]
    public float rayLength = 5f;
    public int damageDealt = 1;
    private float TempGravity;

    [Header("Animation and Movement")]
    public Animator animator;
    public GameObject MainObject;

    [Header("Audio Files")]
    public AudioClip SFXClip1;
    public AudioClip SFXClip2;
    public AudioClip SFXClip3;

    public float volume;

    private bool canAttack = true;
    public bool isAttacking { get; private set; }

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private float initialGravity;

    private void Start()
    {
        playerMovement = MainObject.GetComponent<PlayerMovement>();
        rb = MainObject.GetComponent<Rigidbody2D>();

        initialGravity = rb.gravityScale;
    }

    private void Update()
    {
        // Use Mathf.Sign to ensure the ray goes in the correct direction based on the player's facing direction
        Vector2 rayDirection = Vector2.right * Mathf.Sign(MainObject.transform.localScale.x);

        // Draw the ray for visualization
        Debug.DrawRay(transform.position, rayDirection * rayLength, Color.red);

        if (canAttack && (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.J)))
        {
            playerMovement.StopMovement();
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        playerMovement.canMove = false;
        
        animator.SetTrigger("Attack");

        if (!playerMovement.isGrounded)
        {
            rb.gravityScale = TempGravity;
        }

        // Raycast based on the player's facing direction
        Vector2 rayDirection = Vector2.right * Mathf.Sign(MainObject.transform.localScale.x);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, rayDirection, rayLength);

        AudioRandomizer();

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
            rb.gravityScale = initialGravity;
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

    private void AudioRandomizer()
    {
        int randomNumber = Random.Range(1, 4);

        if (randomNumber == 1)
        {
            SFXManager.instance.PlaySFX(SFXClip1, volume);
        }
        else if (randomNumber == 2)
        {
            SFXManager.instance.PlaySFX(SFXClip2, volume);
        }
        else if (randomNumber == 3)
        {
            SFXManager.instance.PlaySFX(SFXClip3, volume);
        }
    }
}
