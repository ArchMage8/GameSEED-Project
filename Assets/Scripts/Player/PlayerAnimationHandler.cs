using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerCombatHandler combatHandler;

    public void Update()
    {
        if(rb.velocity.x != 0)
        {
            //Walk
            animator.SetFloat("X", 1);
            animator.SetFloat("Y", 0);
        }

        else if (Input.GetButtonDown("Jump"))
        {
            //Jump
            animator.SetFloat("X", 1);
            animator.SetFloat("Y", 0);
        }

        else if(rb.velocity.y < 0)
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", -1);
        }

        else if (combatHandler.isAttacking)
        {
            animator.SetFloat("X", 1);
            animator.SetFloat("Y", 1);
        }

        else
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 0);
        }
    }
}
