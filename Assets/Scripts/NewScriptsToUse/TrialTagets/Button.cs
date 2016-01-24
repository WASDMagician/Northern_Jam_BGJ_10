using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public SlidingPlatform slidingPlatform;

    private bool isActivated;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GrappleHook") && isActivated == false)
        {
            isActivated = true;
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.black;
            slidingPlatform.Activate(isActivated);
        }
    }
}
