using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class roamtest : MonoBehaviour {

    //public Transform target;
    NavMeshAgent agent;
    private Vector3 startPosition;
    public List<Vector3> roamPositions;
    private int roamPositionIndex = 0;
    private Vector3 currentPosition;
    public bool isAtPosition = false;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
	}
	
	void Update () {
        //agent.SetDestination(target.position);
        Roam();
	}

    void Roam()
    {
        Vector3 position = transform.position;

        if(roamPositions != null && roamPositions.Count > 1)
        {
            Vector3 destination = roamPositions[roamPositionIndex];

            if(destination != position && currentPosition != destination)
            { 
                agent.SetDestination(destination);
                currentPosition = destination;
                isAtPosition = false;
            }
            else if (!isAtPosition)
            {
                isAtPosition = true;
                StartCoroutine(Wait(5.0f));
            }
        }
        else if(position != startPosition)
        {
            agent.SetDestination(startPosition);
        }
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (roamPositionIndex < roamPositions.Count - 1)
        {
            roamPositionIndex++;
        }
        else
        {
            roamPositionIndex = 0;
        }
        isAtPosition = false;
    }
}
