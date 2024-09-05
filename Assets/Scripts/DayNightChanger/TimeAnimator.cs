using UnityEngine;

public class TimeAnimator : MonoBehaviour
{
    public Animator canvasAnimator;
    public GameObject canvasObject;

    public void AddTime()
    {
        TimeCycle.Instance.IncreaseTimeValue();
    }

    public void FadeOut()
    {
        canvasAnimator.SetTrigger("FadeOut");
    }
}
