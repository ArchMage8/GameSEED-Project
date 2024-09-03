using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    private WeaponHandler Handler;

    [Header("Weapon To Switch to (Pick One: )")]
    public bool setActiveSickle;
    public bool setActiveLasso;
    public bool setActiveKnife;
    private bool Toggled = false;
    private bool playerInRange = false;

    private void Start()
    {
        ValidateSetup();
    }

    private void Update()
    {
        if (!Toggled && playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L))
            {
                ChangeWeapon();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Toggled && collider.CompareTag("Player"))
        {
            Handler = collider.GetComponent<WeaponHandler>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Handler = null;
            playerInRange = false;
        }
    }

    private void ValidateSetup()
    {
        int trueCount = 0;
        if (setActiveSickle) trueCount++;
        if (setActiveLasso) trueCount++;
        if (setActiveKnife) trueCount++;

        if (trueCount == 0)
        {
            Debug.LogError("Pick 1 weapon to switch to.");
        }
        else if (trueCount > 1)
        {
            Debug.LogError("Error: More than one bool is set to true.");
        }
    }

    private void ChangeWeapon()
    {
        if (Handler == null)
        {
            Debug.LogError("WeaponHandler component not found.");
        }
        else
        {
            Toggled = true;
            Handler.SetActiveBool(setActiveSickle, setActiveLasso, setActiveKnife);
            Debug.Log("Weapon changed.");
        }
    }
}
