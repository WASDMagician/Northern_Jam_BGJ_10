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
            leftDoor.position = new Vector3(leftDoor.position.x - doorShift, leftDoor.position.y, leftDoor.position.z);
            rightDoor.position = new Vector3(rightDoor.position.x + doorShift, rightDoor.position.y, rightDoor.position.z);
            yield return new WaitForSeconds(doorDelay);
        }
    }

    IEnumerator CloseDoor()
    {
        for (int i = 0; i < NoOfDoorShifts; i++)
        {
            leftDoor.position = new Vector3(leftDoor.position.x + doorShift, leftDoor.position.y, leftDoor.position.z);
            rightDoor.position = new Vector3(rightDoor.position.x - doorShift, rightDoor.position.y, rightDoor.position.z);
            yield return new WaitForSeconds(doorDelay);
        }
    }
}
