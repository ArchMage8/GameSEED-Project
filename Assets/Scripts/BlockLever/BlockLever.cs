using UnityEngine;

public class BlockLever : MonoBehaviour
{
    [Header("References")]
    public BlockMain block;
    public GameObject knifePrefab;

    [Header("State Variables")]
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)))
        {
            block.MoveBlock();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
        else if (other.CompareTag("Knife"))
        {
            Debug.Log("T3est");
            block.MoveBlock();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
