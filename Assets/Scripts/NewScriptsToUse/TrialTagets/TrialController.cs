using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrialController : MonoBehaviour
{
    public SlidingPlatform slidingPlatform;
    public TrialTargets[] trialTargets;
    [Range(10.0f, 20.0f)]
    public float time;
    public GameObject screenCanvas;
    public Text timerText;

    private float maxTime;
    private bool timerActive;
    private bool passed;

	void Start ()
    {
        maxTime = time;
        screenCanvas.SetActive(false);
    }
	
	void Update ()
    {
        if(passed == false)
        {
            if(trialTargets[0].IsActivated() || trialTargets[1].IsActivated() || trialTargets[2].IsActivated())
            {
                if(timerActive == false)
                {
                    timerActive = true;
                    screenCanvas.SetActive(true);
                    InvokeRepeating("Timer", 0, 0.01f);
                }
            }

            if (trialTargets[0].IsActivated() && trialTargets[1].IsActivated() && trialTargets[2].IsActivated())
            {
                time = 0.0f;
                passed = true;
                timerActive = false;
            }
            if(timerActive == true)
            {
                timerText.text = "Time Left : " + (time * 1000).ToString("00:000");
            }
        }
    }

    void Timer()
    {
        time -= 0.01f;
        if(time <= 0)
        {
            slidingPlatform.Activate(true);
            for (int i = 0; i < trialTargets.Length; i++)
            {
                trialTargets[i].Reset();
                trialTargets[i].Passed();
                time = maxTime;
                timerText.text = "Congratulations! You Passed";
                CancelInvoke("Timer");
                Invoke("KillCanvas", 5);
            }
        }
    }

    void KillCanvas()
    {
        screenCanvas.SetActive(false);
    }
}
