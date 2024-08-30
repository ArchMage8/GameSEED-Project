using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public float leftOffset;
    public float rightOffset;
    public float patrolSpeed = 2f;
    public float detectionRange = 5f;
    public float StopDuration = 2f;
    public LayerMask playerLayer;

    private Vector3 leftPatrolPoint;
    private Vector3 rightPatrolPoint;
    private bool movingRight = true;
    private bool Detected;

    private void Start()
    {
        leftPatrolPoint = transform.position + Vector3.left * leftOffset;
        rightPatrolPoint = transform.position + Vector3.right * rightOffset;
    }

    private void Update()
    {
        CheckForPlayer();

        if (Detected)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPatrolPoint, patrolSpeed * Time.deltaTime);
            if (transform.position == rightPatrolPoint)
            {
                StartCoroutine(MoveLeft());
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPatrolPoint, patrolSpeed * Time.deltaTime);
            if (transform.position == leftPatrolPoint)
            {
                StartCoroutine(MoveRight());
            }
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void CheckForPlayer()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, playerLayer);
        Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Detected = true;
        }
    }

    private IEnumerator MoveLeft()
    {
        yield return new WaitForSeconds(StopDuration);
        movingRight = false;
        Flip();
    }

    private IEnumerator MoveRight()
    {
        yield return new WaitForSeconds(StopDuration);
        movingRight = true;
        Flip();
    }

    private void ChasePlayer()
    {
       
    }
}
