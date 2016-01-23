using UnityEngine;
using System.Collections;

public class PlatformLever : MonoBehaviour
{
    /// <summary>
    /// require the world canvas and platform you want to move.
    /// press E in trigger to move platform.
    /// </summary>
    public MovingPlatform platform;
    public GameObject WSCanvas;

    private bool inTrigger;

	void Start ()
    {
        WSCanvas.SetActive(false);
    }
	
    void Update()
    {
        if (inTrigger == true && Input.GetKeyDown(KeyCode.E) && platform.IsStationary() == true) 
        {
            platform.MovePlatform();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inTrigger = true;
            WSCanvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
            WSCanvas.SetActive(false);
        }
    }
}
