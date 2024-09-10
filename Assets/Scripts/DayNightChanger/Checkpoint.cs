using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint instance;

    [SerializeField] private Transform defaultCheckpoint;  // Set via Inspector
    private Transform currentCheckpoint;

    private GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentCheckpoint = defaultCheckpoint;  // Initialize current checkpoint to default
        player = GameObject.FindWithTag("Player");  // Assuming player is tagged as "Player"
    }

    public void UpdateCheckpoint(Transform newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;  // Update the current checkpoint to the new one
    }

    public void Respawn()
    {
        if (player != null)
        {
            player.transform.position = currentCheckpoint.position;  // Teleport player to the current checkpoint position

            PlayerMovement temp = player.GetComponent<PlayerMovement>();

            temp.canMove = true;
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
        Debug.LogWarning("The level loader fade isnt made yet :p");
    }
}
