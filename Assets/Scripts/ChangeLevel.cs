using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeLevel : MonoBehaviour {

    Animator animator;
    public Text text;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator FadeOutLevel(string nextLevelName)
    {
        text.text = "Gem collected!";
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
        text.enabled = true;
        StartCoroutine(FadeOutLevel(nextLevelName));
    }
}
