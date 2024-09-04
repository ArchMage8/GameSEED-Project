using UnityEngine;

public class PlayerSwapper : MonoBehaviour
{
    [Header("Weapon Objects")]
    public GameObject sickleWeapon;
    public GameObject lassoWeapon;
    public GameObject knifeWeapon;

    [Header("Default Player")]
    public GameObject defaultPlayer;

    public void WeaponSwap(bool activateSickle, bool activateLasso, bool activateKnife)
    {
        defaultPlayer.SetActive(false);
        sickleWeapon.SetActive(activateSickle);
        lassoWeapon.SetActive(activateLasso);
        knifeWeapon.SetActive(activateKnife);
    }

    private void Start()
    {
        ResetWeapons();
    }

    private void ResetWeapons()
    {
        sickleWeapon.SetActive(false);
        lassoWeapon.SetActive(false);
        knifeWeapon.SetActive(false);
        defaultPlayer.SetActive(true);
    }
}
