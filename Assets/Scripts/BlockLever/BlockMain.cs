using UnityEngine;
using System.Collections;

public class BlockMain : MonoBehaviour
{
    [Header("References")]
    public GameObject[] levers;
    public Transform destination;
    public Sprite openedSprite;
    public Sprite closedSprite;

    [Header("Movement Settings")]
    public float moveSpeed = 1f;
    public float playerSpeedScale = 3f;

    [Header("State Variables")]
    private Vector3 originalPosition;
    private bool isMoving = false;
    private bool isOpen = false;
    private GameObject player;


    private void Start()
    {
        originalPosition = transform.position;
    }

    public void MoveBlock()
    {
        if (isMoving) return;

        StartCoroutine(Move());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            player.transform.parent = transform;

            PlayerMovement temp = player.GetComponentInParent<PlayerMovement>();
            temp.speed *= playerSpeedScale;
            //Speed up logic
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                player.transform.parent = null;
                player = null;
                PlayerMovement temp = player.GetComponentInParent<PlayerMovement>();
                temp.speed /= playerSpeedScale;
            }
        }
    }

    private IEnumerator Move()
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
