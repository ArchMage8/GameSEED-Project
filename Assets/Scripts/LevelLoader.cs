using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int destinationScene;
    [SerializeField] private int transitionDelay;
    [SerializeField]private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void GoToNext()
    {
        StartCoroutine(Loader());
    }

    private IEnumerator Loader()
    {
        animator.SetTrigger("LoadScene");
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(destinationScene);
    }
}
