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
        Debug.Log("Test");
        Target.SetActive(true);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Detector)
        {
            Debug.Log("Test");
            Target.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Detector)
        {
            Target.SetActive(true);
        }
    }
}
