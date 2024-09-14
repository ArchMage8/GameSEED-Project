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

    private float initialPlayerSpeed;
    private bool isOnPlatform = false;

    //Random Privates
    private PlayerMovement playerMovement;
    private PlayerAnimationHandler animationHandler;

    private void Start()
    {
        originalPosition = transform.position;
    }



    private void Update()
    {
        //Handle the player being on the block
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            animationHandler = player.GetComponent<PlayerAnimationHandler>();

            initialPlayerSpeed = playerMovement.speed;

            HandlePlayer();
        }

        else
        {
            playerMovement = null;
            animationHandler = null;

            isOnPlatform = false;
        } 


       //If the player falls on the block it needs to become idle
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
          
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;

            if (player != null)
            {
                player.transform.parent = null;
                player = null;
                
            }
        }
    }

    private IEnumerator Move()
    {
        isMoving = true;

        Vector3 targetPosition = isOpen ? originalPosition : destination.position;
        UpdateLevers();
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

    private void HandlePlayer()
    {
        if(isOnPlatform && isMoving)
        {
            playerMovement.speed = initialPlayerSpeed * playerSpeedScale;
        }

        if(player.GetComponent<Rigidbody2D>().velocity.y == 0 && isOnPlatform)
        {
            
           animationHandler.ForceIdle();
        }
    }
}
