using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeLevel : MonoBehaviour {

    Animator animator;
    public Text text;
    public string level_name = "";
    public int level_num = -1;

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

    void Load_Level()
    {
        if(level_name != "")
        {
            Application.LoadLevel(level_name);
        }
        else if(level_num != -1)
        {
            Application.LoadLevel(level_num);
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
    }
}
