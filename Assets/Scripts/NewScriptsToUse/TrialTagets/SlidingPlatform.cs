using UnityEngine;
using System.Collections;

public class SlidingPlatform : MonoBehaviour {

    public Transform start;
    public Transform end;

    private bool isActivated;
    private float lerpAmount;
	
	void Update ()
    {
	    if(isActivated == true)
        {
            transform.position = Vector3.Lerp(start.position, end.position, lerpAmount);
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(start.position, new Vector3(1, 1, 1));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(end.position, new Vector3(1, 1, 1));
    }

    public void Activate(bool active)
    {
        isActivated = active;
        StartCoroutine(LerpPlatform());
    }

    IEnumerator LerpPlatform()
    {
        for(int i = 0; i < 100; i++)
        {
            lerpAmount += 0.01f;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
