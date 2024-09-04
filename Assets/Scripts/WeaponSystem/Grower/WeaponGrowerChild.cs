using UnityEngine;

public class WeaponGrowerChild : MonoBehaviour
{
    [Header("Growth Stages")]
    public GameObject ungrownVersion;
    public GameObject grownVersion;

    private int startTimeValue;
    private bool isGrowing;

    private void OnEnable()
    {
        isGrowing = true;
        startTimeValue = TimeCycle.Instance.TimeValue;
        ungrownVersion.SetActive(true);
        grownVersion.SetActive(false);
    }

    private void Update()
    {
        if (isGrowing && TimeCycle.Instance.TimeValue > startTimeValue)
        {
            GrowWeapon();
        }
    }

    private void GrowWeapon()
    {
        isGrowing = false;
        ungrownVersion.SetActive(false);
        grownVersion.SetActive(true);
        ResetWeaponGrowerChild();
    }

    public void ResetWeaponGrowerChild()
    {
        isGrowing = false;
        ungrownVersion.SetActive(false);
        grownVersion.SetActive(false);
        gameObject.SetActive(false);
    }
}
