using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint instance;

    [SerializeField] private Transform defaultCheckpoint;  // Set via Inspector
    [SerializeField] private GameObject RespawnCanvas;
    [SerializeField] private float RespawnCanvasDelay;
    [SerializeField] private GameObject DeathCanvas;
    [SerializeField] private GameObject Barrier;

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
        RespawnCanvas.SetActive(false);
    }

    public void UpdateCheckpoint(Transform newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;  // Update the current checkpoint to the new one
    }

    public void Respawn()
    {
        DeathCanvas.SetActive(false);
        Barrier.SetActive(false);
        StartCoroutine(RespawnWithDelay());
    }

    private void RespawnLogic()
    {
        if (player != null)
        {
            player.transform.position = currentCheckpoint.position;  // Teleport player to the current checkpoint position

            PlayerMovement temp = player.GetComponent<PlayerMovement>();
            HealthSystem healthTemp = player.GetComponent<HealthSystem>();

            temp.canMove = true;
            healthTemp.Health = 5;
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
        Debug.LogWarning("The level loader fade isnt made yet :p");
    }

    private IEnumerator RespawnWithDelay()
    {
        RespawnCanvas.SetActive(true);
        RespawnLogic();
        yield return new WaitForSeconds(RespawnCanvasDelay);
        RespawnCanvas.SetActive(false);

    }
}
