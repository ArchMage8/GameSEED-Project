using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizonal;

    [Header("Controls: ")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
   
    private bool isFacingRight = true;
    [Space(30)]

    private Rigidbody2D rb;
    [Header("System: ")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    public bool canMove = true;

    public bool isMoving { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
}
