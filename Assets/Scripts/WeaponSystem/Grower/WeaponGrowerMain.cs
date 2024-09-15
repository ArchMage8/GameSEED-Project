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

    [Header("Audio Files")]
    public AudioClip SFXClip;
    public float volume;

    private SpriteRenderer spriteRenderer;

    private bool playerInRange;
    private bool isPlanting;
    private bool canPlant = true;

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
            //spriteRenderer.enabled = false;
            ActivatePlantingMode();
        }

        if (isPlanting && canPlant)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && WeaponManager.Instance.CanGrowSickle)
            {
                PlantWeapon(sickleChild);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && WeaponManager.Instance.CanGrowLasso)
            {
                PlantWeapon(lassoChild);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && WeaponManager.Instance.CanGrowKnife)
            {
                PlantWeapon(knifeChild);
            }
        }
    }

    private void ActivatePlantingMode()
    {
        Debug.Log("testing1");

        sickleImage.SetActive(WeaponManager.Instance.CanGrowSickle);
        lassoImage.SetActive(WeaponManager.Instance.CanGrowLasso);
        knifeImage.SetActive(WeaponManager.Instance.CanGrowKnife);
    }

    private void PlantWeapon(GameObject weaponChild)
    {
        weaponChild.SetActive(true);
        SFXManager.instance.PlaySFX(SFXClip, volume);
        canPlant = false;

        sickleImage.SetActive(false);
        lassoImage.SetActive(false);
        knifeImage.SetActive(false);
        //ResetGrower();
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
            if (!isPlanting)
            {
                ResetGrower();
            }
        }
    }

    public void ResetGrower()
    {
        playerInRange = false;
        isPlanting = false;
        canPlant = true;
        spriteRenderer.enabled = true;

        sickleImage.SetActive(false);
        lassoImage.SetActive(false);
        knifeImage.SetActive(false);

        sickleChild.SetActive(false);
        lassoChild.SetActive(false);
        knifeChild.SetActive(false);
    }
}
