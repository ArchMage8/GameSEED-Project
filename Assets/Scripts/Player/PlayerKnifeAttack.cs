using System.Collections;
using UnityEngine;

public class PlayerKnifeAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damageDealt = 1;
    public float generationDelay = 0.5f;
    private float TempGravity;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    [Header("Animation and Movement")]
    public Animator playerAnimator;
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

        if (!playerMovement.isGrounded)
        {
            rb.gravityScale = TempGravity;
        }

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
            rb.gravityScale = initialGravity;
            StartCoroutine(MidAirHandler());
        }
    }

    private void Throw()
    {
        AudioRandomizer();
        StartCoroutine(GenerateProjectile());
    }

    private IEnumerator GenerateProjectile()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Get the player's X scale and apply it to the projectile's scale (for visual purposes)
        Vector3 playerScale = MainObject.transform.localScale;
        projectile.transform.localScale = new Vector3(playerScale.x, projectile.transform.localScale.y, projectile.transform.localScale.z);

        // Set projectile velocity
        Vector2 direction = playerScale.x > 0 ? Vector2.right : Vector2.left;
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
