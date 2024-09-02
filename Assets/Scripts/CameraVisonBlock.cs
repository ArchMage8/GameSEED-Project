using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisonBlock : MonoBehaviour
{
    [Header("Object References")]
    public GameObject Detector;
    public GameObject Target;

    private void Start()
    {
        if (Target != null)
        {
            Target.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Detector && Target != null)
        {
            Target.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Detector && Target != null)
        {
            Target.SetActive(false);
        }
    }
}
