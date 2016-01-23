using UnityEngine;
using System.Collections;

public class OpenDoors : MonoBehaviour
{

    public Transform leftDoor;
    public Transform rightDoor;

    private int NoOfDoorShifts;
    private float doorShift;
    private float doorDelay;

    void Start ()
    {
        NoOfDoorShifts = 30;
        doorShift = 0.1f;
        doorDelay = 0.025f;
    }
	
	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(OpenDoor());
        }       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        for(int i = 0; i < NoOfDoorShifts; i++)
        {
            leftDoor.localPosition = new Vector3(leftDoor.localPosition.x - doorShift, leftDoor.localPosition.y, leftDoor.localPosition.z);
            rightDoor.localPosition = new Vector3(rightDoor.localPosition.x + doorShift, rightDoor.localPosition.y, rightDoor.localPosition.z);
            yield return new WaitForSeconds(doorDelay);
        }
    }

    IEnumerator CloseDoor()
    {
        for (int i = 0; i < NoOfDoorShifts; i++)
        {
            leftDoor.localPosition = new Vector3(leftDoor.localPosition.x + doorShift, leftDoor.localPosition.y, leftDoor.localPosition.z);
            rightDoor.localPosition = new Vector3(rightDoor.localPosition.x - doorShift, rightDoor.localPosition.y, rightDoor.localPosition.z);
            yield return new WaitForSeconds(doorDelay);
        }
    }
}
