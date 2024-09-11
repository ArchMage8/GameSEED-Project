using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisonBlock : MonoBehaviour
{
    [Header("Object References")]
    public GameObject Detector;
    public GameObject Target;

    private bool TestBool = false;

    private void Start()
    {
        Debug.Log("Test");
        Target.SetActive(true);
        
    }

    private void Update()
    {
        if (TestBool)
        {
            Debug.Log("test2");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Detector)
        {
            Debug.Log("Enter");
            Target.SetActive(false);
            TestBool = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Detector)
        {
            Debug.Log("Exit");
            Target.SetActive(true);
        }
    }
}
