using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeLevel : MonoBehaviour {

    public Animator animator;
    public Text text;
    public string level_name = "";
    public int level_num = -1;

    void Awake()
    {
        //animator = GameObject.FindGameObjectWithTag("FadeLevel").GetComponent<Animator>();
    }

    IEnumerator FadeOutLevel()
    {
        //text.text = "Gem collected!";
        FadeOut();
        yield return new WaitForSeconds(1.5f);
        Load_Level();
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
        StartCoroutine(FadeOutLevel());
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

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("test");
        StartCoroutine(FadeOutLevel());
    }
    
}
