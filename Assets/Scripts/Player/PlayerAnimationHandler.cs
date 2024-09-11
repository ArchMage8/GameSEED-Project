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

        else if (Input.GetKeyDown(KeyCode.Space))
        {
           StartCoroutine(JumpLogic());
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

        else if(rb.velocity.y == 0 && rb.velocity.x == 0)
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 0);
        }
    }

    private IEnumerator JumpLogic()
    {
        yield return null;
        while (rb.velocity.y > 0)
        {
            Debug.Log("Jump");
            animator.SetFloat("X", 1);
            animator.SetFloat("Y", 0);
            yield return null;

        }
    }
}
