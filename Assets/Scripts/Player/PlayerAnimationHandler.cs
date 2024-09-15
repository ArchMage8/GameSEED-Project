using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerMovement playerMovement;
    public Animator animator;
    public GameObject CombatHandler;
    private EnemyCombatHandler combatHandler;
    public bool Default;

    private bool isFalling = false;
    private bool isJumping = false; // New flag to check if jumping

    private void Start()
    {
        if (!Default)
        {
            combatHandler = CombatHandler.GetComponent<EnemyCombatHandler>();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Start jump logic and cancel walking animation
            StartCoroutine(JumpLogic());
            return; // Return immediately to prevent other logic (like walking) from running during the jump
        }

        if (!isJumping && rb.velocity.x != 0 && playerMovement.isGrounded)
        {
            // Walk
            if (!isFalling)
            {
               
                    animator.SetFloat("X", 1);
                    animator.SetFloat("Y", 0);
                
            }
        }

        else if (rb.velocity.y < -0.01)
        {
            //Debug.Log("falling");
            isFalling = true;
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", -1);
        }

        else if (rb.velocity.y == 0 && rb.velocity.x == 0)
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 0);
        }

        if (rb.velocity.y <= 0)
        {
            isFalling = false;
        }
    }

    private IEnumerator JumpLogic()
    {
        isJumping = true; // Set isJumping to true when jump starts

        yield return null;
        while (rb.velocity.y > 0)
        {
            isFalling = false;
            //Debug.Log("Jump");
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 1);
            yield return null;
        }

        // Once the jump is finished, reset the isJumping flag
        isJumping = false;
    }
}
