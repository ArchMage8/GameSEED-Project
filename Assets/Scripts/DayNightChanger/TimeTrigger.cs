using UnityEngine;

public class TimeTrigger : MonoBehaviour
{
    [Header("Canvas GameObject")]
    public GameObject canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeTime();
        }
    }

    private void ChangeTime()
    {
        canvas.SetActive(true);
    }
}
