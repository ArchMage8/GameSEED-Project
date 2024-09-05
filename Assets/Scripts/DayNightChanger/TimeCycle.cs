using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    [Header("Time Variables")]
    public int TimeValue = 1;

    private static TimeCycle instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static TimeCycle Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TimeCycle>();
            }
            return instance;
        }
    }

    public void IncreaseTimeValue()
    {
        TimeValue++;
    }
}
