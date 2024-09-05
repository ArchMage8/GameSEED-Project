using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    [Header("Day/Night GameObjects")]
    public GameObject Day;
    public GameObject Night;

    private void Update()
    {
        if (TimeCycle.Instance.TimeValue % 2 == 1)
        {
            Day.SetActive(true);
            Night.SetActive(false);
        }
        else
        {
            Day.SetActive(false);
            Night.SetActive(true);
        }
    }
}
