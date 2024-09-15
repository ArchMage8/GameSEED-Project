using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizonal;

    [Header("Controls: ")]
    public float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
   
    private bool isFacingRight = true;
    [Space(30)]

    private Rigidbody2D rb;
    [Header("System: ")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    public bool canMove = true;

    public bool isMoving;

    [Header("Audio Files")]
    public AudioClip LandClip;

    [Header("Footstep Settings")]
    public AudioClip footstepClip;
    public AudioSource audioSource;
    public bool isWalking = false;  // The bool that controls whether sound plays

    private PlayerMovement playerMovement;
    private bool canPlaySound = true;

    private bool isFalling;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FallHandler();

        if (TimeCycle.Instance.isChanging == true)
        {
            canMove = false;
        }

        else
        {
            canMove = true;
        }

        horizonal = Input.GetAxisRaw("Horizontal");

        isMoving = horizonal != 0f;

        Flip();

        if (Input.GetButtonDown("Jump") && IsGrounded() && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        isGrounded = IsGrounded();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(horizonal * speed, rb.velocity.y);

            if (isGrounded && isMoving && canPlaySound && rb.velocity.y == 0)
            {
                StartCoroutine(PlayFootstepSound());
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizonal < 0f || !isFacingRight && horizonal > 0f && canMove)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void StopMovement()
    {
        horizonal = 0f;
        rb.velocity = Vector2.zero;
        isMoving = false;
    }

    private IEnumerator fallSound()
    {
        while (isFalling)
        {
            yield return null;
        }
        Debug.Log("land");
        SFXManager.instance.PlaySFX(LandClip);
    }

    private void FallHandler()
    {
        if (isGrounded)
        {
            isFalling = false;
        }

        else if (rb.velocity.y < 0 && !isGrounded)
        {
            //falling logic
            StartCoroutine(fallSound());
            isFalling = true;

        }

        
    }

    private IEnumerator PlayFootstepSound()
    {
        canPlaySound = false;
        audioSource.PlayOneShot(footstepClip);
        yield return new WaitForSeconds(footstepClip.length);  // Ensures 0.5s delay (or the clip length)
        canPlaySound = true;
    }
}
