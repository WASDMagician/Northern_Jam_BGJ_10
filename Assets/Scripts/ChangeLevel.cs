using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator FadeOutLevel(string nextLevelName)
    {
        FadeOut();
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel(nextLevelName);
    }

    public void FadeOut()
    {
        animator.Play("FadeOut");
    }

    public void FadeIn()
    {
        animator.Play("FadeIn");
    }

    public void ChangeToLevel(string nextLevelName)
    {
        StartCoroutine(FadeOutLevel(nextLevelName));
    }
}
