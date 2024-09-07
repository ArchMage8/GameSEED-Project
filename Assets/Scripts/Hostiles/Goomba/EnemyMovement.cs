using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float leftOffset;
    public float rightOffset;
    public float patrolSpeed = 2f;
    public float StopDuration = 2f;
    [Space(20)]

    [Header("Chase Settings")]
    public float detectionRange = 5f;
    public float ChaseSpeedScaler = 1.5f;
    [Space(20)]

    [Header("Layer Settings")]
    public LayerMask playerLayer;

    private Vector3 leftPatrolPoint;
    private Vector3 rightPatrolPoint;
    private bool movingRight = true;
    public bool Detected;
    private bool canPatrol = true;
    private bool canFlip = true;
    private bool isMoving = false;
    public Animator animator;

    private EnemyCombatHandler enemyCombatHandler;

    private void Start()
    {
        leftPatrolPoint = transform.position + Vector3.left * leftOffset;
        rightPatrolPoint = transform.position + Vector3.right * rightOffset;

        enemyCombatHandler = GetComponent<EnemyCombatHandler>();
    }

    private void Update()
    {
        CheckForPlayer();
        updateAnimation();

        if (enemyCombatHandler.CanMove)
        {

            if (Detected)
            {
                ChasePlayer();
            }
            else if (canPatrol)
            {
                Patrol();
            }
        }
    }

    private void Patrol()
    {
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPatrolPoint, patrolSpeed * Time.deltaTime);
            if (transform.position == rightPatrolPoint)
            {
                StartCoroutine(ChangeDirection());
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPatrolPoint, patrolSpeed * Time.deltaTime);
            if (transform.position == leftPatrolPoint)
            {
                StartCoroutine(ChangeDirection());
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

    private IEnumerator ChangeDirection()
    {

        if (canFlip)
        {
            isMoving = false;
            canFlip = false;
            yield return new WaitForSeconds(StopDuration);
            isMoving = true;
            canPatrol = true;
            if (movingRight)
            {
                movingRight = false;
                Flip();
            }

            else if (!movingRight)
            {
                movingRight = true;
                Flip();
            }
        canFlip = true;
        }
    }

    private void ChasePlayer()
    {
        canPatrol = false;
        
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 targetPosition = new Vector3(playerPosition.x, transform.position.y, transform.position.z);

        if (movingRight && transform.position.x < rightPatrolPoint.x ||
            !movingRight && transform.position.x > leftPatrolPoint.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * ChaseSpeedScaler * Time.deltaTime);
        }
        else
        {
            Detected = false;
            StartCoroutine(ChangeDirection());
        }
    }

    private void updateAnimation()
    {
       animator.SetBool("IsWalking", isMoving);
    }
}
