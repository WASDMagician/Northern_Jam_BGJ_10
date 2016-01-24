using UnityEngine;
using System.Collections;

public class TrialTargets : MonoBehaviour {

    public TrialController trialCont;
    private Renderer rend;
    private Collider col;
    private bool isActivated;
    private bool passed;
    private bool wait;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        rend.material.color = Color.red;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrappleHook") && isActivated == false && passed == false && wait == false)
        {
            isActivated = true;
            rend.material.color = Color.green;
            col.enabled = false;
            trialCont.incProgress();
        }
    }

    public void Reset()
    {
        isActivated = false;
        rend.material.color = Color.red;
        col.enabled = true;
    }

    public void Wait(int time)
    {
        wait = true;
        rend.material.color = Color.gray;
        Invoke("ReActivate", time);
    }

    public bool IsActivated()
    {
        return isActivated;
    }

    public void Passed()
    {
        passed = true;
        rend.material.color = Color.black;
    }

    void ReActivate()
    {
        wait = false;
        Reset();
    }
}
