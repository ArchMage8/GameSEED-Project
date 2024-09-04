using System.Collections;
using UnityEngine;

public class LeverBlockSystem : MonoBehaviour
{
    // Block Movement
    public GameObject block;
    public Vector3 targetPosition;
    public float moveSpeed = 2f;

    // Interaction
    public GameObject knifePrefab;
    public LayerMask playerLayer;

    private Vector3 originalPosition;
    private bool isInteracting;

    private void Start()
    {
        originalPosition = block.transform.position;
    }

    private void Update()
    {
        CheckPlayerInteraction();
    }

    private void CheckPlayerInteraction()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 1f, playerLayer);
        if (playerCollider != null && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)))
        {
            MoveBlock();
            FlipLever();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == knifePrefab)
        {
            MoveBlock();
            FlipLever();
        }
    }

    private void MoveBlock()
    {
        StartCoroutine(MoveBlockCoroutine());
    }

    private IEnumerator MoveBlockCoroutine()
    {
        Vector3 startPosition = block.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            block.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        block.transform.position = targetPosition;
    }

    private void FlipLever()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }
}
