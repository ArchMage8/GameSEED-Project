using System.Collections;
using UnityEngine;

public class PlayerKnifeAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    //public float rayLength = 5f;
    public int damageDealt = 1;
    public float generationDelay = 0.5f;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    [Header("Animation and Movement")]
    public Animator playerAnimator;
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
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;
        playerAnimator.SetTrigger("Attack");

        playerMovement.canMove = false;

        playerMovement.StopMovement();

        Throw();

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

    private void Throw()
    {
        StartCoroutine(GenerateProjectile());
    }

    private IEnumerator GenerateProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Vector2 direction = MainObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction * projectileSpeed;

        yield return new WaitForSeconds(generationDelay);
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
