using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public GameObject LevelLoaderCanvas;
    private LevelLoader levelLoader;
    public bool PlayerDetect = false;

    private void Start()
    {
        levelLoader = LevelLoaderCanvas.GetComponent<LevelLoader>();
    }

    private void Update()
    {
        if (PlayerDetect)
        {
            levelLoader.GoToNext();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDetect = true;
        }
    }
}
