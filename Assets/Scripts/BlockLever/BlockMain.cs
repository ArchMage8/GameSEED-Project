using UnityEngine;

public class BlockMain : MonoBehaviour
{
    [Header("References")]
    public GameObject[] levers;
    public Transform destination;
    public Sprite openedSprite;
    public Sprite closedSprite;

    [Header("Movement Settings")]
    public float moveSpeed = 1f;

    [Header("State Variables")]
    private Vector3 originalPosition;
    private bool isMoving = false;
    private bool isOpen = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void MoveBlock()
    {
        if (isMoving) return;

        StartCoroutine(Move());
    }

    private System.Collections.IEnumerator Move()
    {
        isMoving = true;

        Vector3 targetPosition = isOpen ? originalPosition : destination.position;
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isOpen = !isOpen;

        UpdateLevers();

        isMoving = false;
    }

    private void UpdateLevers()
    {
        Sprite newSprite = isOpen ? openedSprite : closedSprite;
        foreach (GameObject lever in levers)
        {
            lever.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}
