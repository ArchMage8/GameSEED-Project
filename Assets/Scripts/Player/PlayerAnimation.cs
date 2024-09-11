using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public Animator animator;

    private void Update()
    {
        if (rb != null)
        {
            if (rb.velocity.x != 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            }

            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isJump", true);
            }
            else
            {
                animator.SetBool("isJump", false);
            }

            if(rb.velocity.y < 0)
            {
                animator.SetBool("isFalling", true);
            }
            else
            {
                animator.SetBool("isFalling", false);
            }
 
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found!");
        }
    }
}
