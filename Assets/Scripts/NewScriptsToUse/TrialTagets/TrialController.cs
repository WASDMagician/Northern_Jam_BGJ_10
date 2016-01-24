using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrialController : MonoBehaviour
{
    public SlidingPlatform slidingPlatform;
    public TrialTargets[] trialTargets;
    [Range(10.0f, 60.0f)]
    public float time;
    public GameObject screenCanvas;
    public Text timerText;

    private float maxTime;
    private bool timerActive;
    private bool passed;

    // bar
    public RectTransform ProgressBarTransform;
    private float ProgressBarY;
    private float minimumProgressBarX;
    private float maximumProgressBarX;
    private int curProg;

    void Start ()
    {
        curProg = 0;
        maxTime = time;
        InitializeBar();
        UpdateProgressBar();
        screenCanvas.SetActive(false);
    }
	
	void Update ()
    {
        if(passed == false)
        {
            /*if (trialTargets[0].IsActivated() && trialTargets[1].IsActivated() && trialTargets[2].IsActivated())
            {
                time = 0.0f;
                passed = true;
                timerActive = false;
            }
            else if(trialTargets[0].IsActivated() && trialTargets[1].IsActivated() || trialTargets[1].IsActivated() && trialTargets[2].IsActivated() || trialTargets[0].IsActivated() && trialTargets[2].IsActivated())
            {
                curProg++;

            }
            else if(trialTargets[0].IsActivated() || trialTargets[1].IsActivated() || trialTargets[2].IsActivated())
            {
                if(timerActive == false)
                {
                    timerActive = true;
                    screenCanvas.SetActive(true);
                    InvokeRepeating("Timer", 0, 0.01f);
                }
            }*/
            if(timerActive == true)
            {
                timerText.text = "Time Left : " + (time * 1000).ToString("00:000");
            }
        }
    }

    void Timer()
    {
        time -= 0.01f;
        if(time <= 0 && passed == true)
        {
            slidingPlatform.Activate(true);
            curProg = 0;
            for (int i = 0; i < trialTargets.Length; i++)
            {
                trialTargets[i].Reset();
                trialTargets[i].Passed();
                time = maxTime;
                timerText.text = "Congratulations! You Passed";
                CancelInvoke("Timer");
                timerActive = false;
                Invoke("KillCanvas", 5);
            }
        }
        else if(time <= 0 && passed == false)
        {
            curProg = 0;
            for (int j = 0; j < trialTargets.Length; j++)
            {
                trialTargets[j].Wait(5);
                time = maxTime;
                timerText.text = "You Failed...";
                CancelInvoke("Timer");
                timerActive = false;
                Invoke("KillCanvas", 5);
            }
        }
    }

    void KillCanvas()
    {
        screenCanvas.SetActive(false);
    }


    void InitializeBar()
    {
        ProgressBarY = ProgressBarTransform.localPosition.y;
        minimumProgressBarX = ProgressBarTransform.localPosition.x - ProgressBarTransform.rect.width;
        maximumProgressBarX = ProgressBarTransform.localPosition.x;
    }

    void UpdateProgressBar()
    {
        float currentXValue;
        currentXValue = (3 - curProg) * (minimumProgressBarX - maximumProgressBarX) / (3 - 0) + maximumProgressBarX;
        ProgressBarTransform.localPosition = new Vector3(currentXValue, ProgressBarY);
    }

    public void incProgress()
    {
        if(passed == false)
        {
            if (timerActive == false)
            {
                timerActive = true;
                screenCanvas.SetActive(true);
                InvokeRepeating("Timer", 0, 0.01f);
            }
            curProg++;
            UpdateProgressBar();
            if(curProg == trialTargets.Length)
            {
                passed = true;
                time = 0.0f;
                timerActive = false;
            }
        }
    }
}
