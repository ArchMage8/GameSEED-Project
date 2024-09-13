using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    [Header("Day/Night GameObjects")]
    public GameObject[] DayObjects;   // Array for Day objects
    public GameObject[] NightObjects; // Array for Night objects

    private void Update()
    {
        if (TimeCycle.Instance.TimeValue % 2 == 1)
        {
            SetActiveState(DayObjects, true);
            SetActiveState(NightObjects, false);
        }
        else
        {
            SetActiveState(DayObjects, false);
            SetActiveState(NightObjects, true);
        }
    }

    private void SetActiveState(GameObject[] objects, bool state)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(state);
        }
    }
}
