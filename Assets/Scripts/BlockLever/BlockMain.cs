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

    [Header("Audio Files")]
    public AudioClip SFXClip;
    


    [Header("State Variables")]
    private Vector3 originalPosition;
    [HideInInspector] public bool isMoving = false;
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
            

            initialPlayerSpeed = playerMovement.speed;
            isOnPlatform = true;
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
        if (isMoving)
        {
            return;
        }
        else
        {
            SFXManager.instance.PlaySFX(SFXClip);
            StartCoroutine(Move());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            player.transform.parent = transform;

            animationHandler = player.GetComponentInChildren<PlayerAnimationHandler>();

            animationHandler.animator.SetFloat("X", 0);
            animationHandler.animator.SetFloat("Y", 0);

            Debug.Log("Player Detect");
          
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            player.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;

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

        //UpdateLevers();

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
            if (isOnPlatform && isMoving == true)
            {
                //Debug.Log("Test");
                player.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
            }
            
            if(player.GetComponent<Rigidbody2D>().velocity.x == 0)
            {
            animationHandler.animator.SetFloat("X", 0);
            animationHandler.animator.SetFloat("Y", 0);
        }
    }
}
