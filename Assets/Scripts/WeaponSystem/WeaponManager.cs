using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    [Header("Growth Capabilities")]
    public bool CanGrowSickle = false;
    public bool CanGrowLasso = false;
    public bool CanGrowKnife = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Destroy(gameObject);
        }
    }
}
