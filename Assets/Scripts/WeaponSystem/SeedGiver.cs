using UnityEngine;

public class SeedGiver : MonoBehaviour
{
    [Header("Weapon Growth Settings")]
    public bool canGrowSickle;
    public bool canGrowLasso;
    public bool canGrowKnife;

    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Interact()
    {
        if (canGrowSickle)
        {
            WeaponManager.Instance.CanGrowSickle = true;
        }
        else if (canGrowLasso)
        {
            WeaponManager.Instance.CanGrowLasso = true;
        }
        else if (canGrowKnife)
        {
            WeaponManager.Instance.CanGrowKnife = true;
        }
    }
}
