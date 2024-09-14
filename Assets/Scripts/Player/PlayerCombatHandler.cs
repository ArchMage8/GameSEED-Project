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
        if (canAttack && (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.J)))
        {
            StartCoroutine(Attack());
        }
        //Debug.Log(isAttacking);
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        //Debug.Log(isAttacking + "a");
        playerMovement.canMove = false;
        playerMovement.StopMovement();
        animator.SetTrigger("Attack");

        if (!playerMovement.isGrounded)
        {
            rb.gravityScale = TempGravity;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right * transform.localScale.x, rayLength);
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
            SFXManager.instance.PlaySFX(SFXClip1);
        }
        else if (randomNumber == 2)
        {
            SFXManager.instance.PlaySFX(SFXClip2);
        }
        else if (randomNumber == 3)
        {
            SFXManager.instance.PlaySFX(SFXClip3);
        }
    }
}
