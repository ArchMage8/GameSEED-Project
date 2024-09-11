using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIndicator : MonoBehaviour
{
    [SerializeField] private GameObject Indicator;

    private bool inRange = false;

    private void Start()
    {
        Indicator.SetActive(false);
    }
    private void Update()
    {
        if (inRange)
        {
            Indicator.SetActive(true);
        }

        else
        {
            Indicator.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
