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
    public bool playerSpotted = false;

    private Player_Controller player_controller;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        startPosition = transform.position;
	}

    void Update()
    {
        //agent.SetDestination(target.position);

        if(Input.GetKeyDown(KeyCode.T))
        {
            playerSpotted = !playerSpotted;
        }

        if (!playerSpotted)
        {
            Roam();
        }
        else if (playerSpotted)
        {
            Chase();
        }
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
                //random timer on wait
                StartCoroutine(Wait(Random.Range(2.0f, 8.0f)));
            }
        }
        else if(position != startPosition)
        {
            agent.SetDestination(startPosition);
        }
    }

    void Chase()
    {
        Vector3 playerPosition = player_controller.transform.position;
        // Enemy needs to lose interest
        agent.SetDestination(playerPosition);
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
