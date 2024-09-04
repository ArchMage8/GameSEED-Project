using UnityEngine;

public class WeaponGrowerMain : MonoBehaviour
{
    [Header("Weapon Images")]
    public GameObject sickleImage;
    public GameObject lassoImage;
    public GameObject knifeImage;

    [Header("Weapon Grower Child Objects")]
    public GameObject sickleChild;
    public GameObject lassoChild;
    public GameObject knifeChild;

    private SpriteRenderer spriteRenderer;

    private bool playerInRange;
    private bool isPlanting;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetGrower();
    }

    private void Update()
    {
        if (playerInRange && !isPlanting && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)))
        {
            isPlanting = true;
            spriteRenderer.enabled = false;
            ActivatePlantingMode();
        }

        if (isPlanting)
        {
            if (Input.GetKeyDown(KeyCode.A) && WeaponManager.Instance.CanGrowSickle)
            {
                PlantWeapon(sickleChild);
            }
            else if (Input.GetKeyDown(KeyCode.B) && WeaponManager.Instance.CanGrowLasso)
            {
                PlantWeapon(lassoChild);
            }
            else if (Input.GetKeyDown(KeyCode.C) && WeaponManager.Instance.CanGrowKnife)
            {
                PlantWeapon(knifeChild);
            }
        }
    }

    private void ActivatePlantingMode()
    {
        sickleImage.SetActive(true);
        lassoImage.SetActive(true);
        knifeImage.SetActive(true);

        sickleImage.SetActive(!WeaponManager.Instance.CanGrowSickle);
        lassoImage.SetActive(!WeaponManager.Instance.CanGrowLasso);
        knifeImage.SetActive(!WeaponManager.Instance.CanGrowKnife);
    }

    private void PlantWeapon(GameObject weaponChild)
    {
        weaponChild.SetActive(true);
        ResetGrower();
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
            ResetGrower();
        }
    }

    public void ResetGrower()
    {
        playerInRange = false;
        isPlanting = false;
        spriteRenderer.enabled = true;

        sickleImage.SetActive(false);
        lassoImage.SetActive(false);
        knifeImage.SetActive(false);
    }
}
