using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public GameObject Sickle;
    public GameObject Lasso;
    public GameObject Knife;

    public bool isSickleActive;
    public bool isLassoActive;
    public bool isKnifeActive;

    public static PlayerStateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Sickle != null)
        {
            isSickleActive = Sickle.activeSelf;
        }

        if (Lasso != null)
        {
            isLassoActive = Lasso.activeSelf;
        }

        if (Knife != null)
        {
            isKnifeActive = Knife.activeSelf;
        }
    }
}
