using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    public static TimeCycle Instance { get; private set; }

    public int TimeValue { get; private set; }

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

    public void IncreaseTimeValue()
    {
        TimeValue += 1;
    }
}
