using UnityEngine;

public class PlantSwapper : MonoBehaviour
{
    [Header("Target Weapon")]
    public bool targetSickle;
    public bool targetLasso;
    public bool targetKnife;

    private bool playerInRange;

    private void Start()
    {
        ValidateTargetSelection();
    }

    private void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)))
        {
            SwapWeapon();
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

    private void ValidateTargetSelection()
    {
        int selectedCount = 0;
        if (targetSickle) selectedCount++;
        if (targetLasso) selectedCount++;
        if (targetKnife) selectedCount++;

        if (selectedCount > 1)
        {
            Debug.LogError("Only one target weapon can be set to true.");
        }
    }

    private void SwapWeapon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerSwapper playerSwapper = player.GetComponent<PlayerSwapper>();
            if (playerSwapper != null)
            {
                playerSwapper.WeaponSwap(targetSickle, targetLasso, targetKnife);
                WeaponGrowerMain mainGrower = transform.parent.GetComponent<WeaponGrowerMain>();
                if (mainGrower != null)
                {
                    mainGrower.ResetGrower();
                }
            }
        }
    }
}
