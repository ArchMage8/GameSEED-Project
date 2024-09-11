using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerCombatHandler combatHandler;

    private bool isFalling = false;

    public void Update()
    {
        if(rb.velocity.x != 0)
        {
            //Walk
            if (!isFalling)
            {
                animator.SetFloat("X", 1);
                animator.SetFloat("Y", 0);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
           StartCoroutine(JumpLogic());
        }

        else if(rb.velocity.y < -0.01)
        {
            isFalling = true;
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", -1);
        }

        else if (combatHandler.isAttacking)
        {
            animator.SetFloat("X", 1);
            animator.SetFloat("Y", 1);
        }

        else if(rb.velocity.y == 0 && rb.velocity.x == 0)
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 0);
        }

        if(rb.velocity.y == 0)
        {
            isFalling = false;
        }
    }

    private IEnumerator JumpLogic()
    {
        yield return null;
        while (rb.velocity.y > 0)
        {
            isFalling = false;
            Debug.Log("Jump");
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 1);
            yield return null;

        }
    }
}
