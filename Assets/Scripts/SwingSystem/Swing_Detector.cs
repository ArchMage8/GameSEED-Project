using UnityEngine;

public class Swing_Detector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Swing_Player playerSwing = collision.GetComponent<Swing_Player>();
            if (playerSwing != null)
            {
                playerSwing.inRange = true;
                playerSwing.SetSwingTarget(GetComponent<Rigidbody2D>(), transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Swing_Player playerSwing = collision.GetComponent<Swing_Player>();
            if (playerSwing != null)
            {
                playerSwing.inRange = false;
            }
        }
    }
}
