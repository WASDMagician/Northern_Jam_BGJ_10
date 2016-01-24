using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    //public Transform target;
    NavMeshAgent agent;
    private Vector3 startPosition;
    public List<Vector3> roamPositions;
    private int roamPositionIndex = 0;
    private Vector3 currentPosition;
    public bool isAtPosition = false;
    public bool playerSpotted = false;
    private int counter;

    public Player_Controller player_controller;
    public int health;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        startPosition = transform.position;
        agent.speed = Random.Range(2.0f, 3.0f);
        counter = 0;
        health = 30;
    }

    void Update()
    {
        //agent.SetDestination(target.position);

        if(Input.GetKeyDown(KeyCode.T))
        {
            playerSpotted = !playerSpotted;
            //agent.SetDestination(startPosition);
        }

        if (!playerSpotted)
        {
            Roam();
        }
        else if (playerSpotted)
        {
            //broken right now wont return to original starting point
            Chase();
        }

        if (agent.pathStatus == NavMeshPathStatus.PathPartial && !agent.hasPath)
        {
            counter++;
        }

        if (counter >= 300)
        {
            counter = 0;
            playerSpotted = false;
        }
    }

    public void Roam()
    {
        Vector3 position = transform.position;
        //agent.speed = Random.Range(2.0f, 4.0f);
        if (roamPositions != null && roamPositions.Count > 1)
        {
            Vector3 destination = roamPositions[roamPositionIndex];

            //Debug.Log(roamPositionIndex);
            //Debug.Log("destination " + destination + " position " + position);

            if (destination != position && currentPosition != destination)
            { 
                agent.SetDestination(destination);
                currentPosition = destination;
                isAtPosition = false;
                Debug.Log("test");
            }
            else if (position == destination && !isAtPosition)
            {
                isAtPosition = true;
                //random timer on wait
                StartCoroutine(Wait(Random.Range(2.0f, 4.0f)));
            }
            else if (destination != position && !isAtPosition)
            {
                // isAtPosition = true;
                //random timer on wait
                //StartCoroutine(Wait(1.0f));

                //if chased and no longer chasing - return back to normal path
                agent.SetDestination(destination);
                currentPosition = destination;
                isAtPosition = false;
            }

        }
        else if(position != startPosition)
        {
            Debug.Log("not in start pos");
            agent.SetDestination(startPosition);
        }
    }

    public void Chase()
    {
        RotateToPlayer();
        Vector3 playerPosition = (player_controller.transform.position);
        agent.speed = 4.0f;
        agent.SetDestination(playerPosition);
    }

    void RotateToPlayer()
    {
        this.transform.LookAt(new Vector3(player_controller.transform.position.x, transform.position.y, player_controller.transform.position.z));
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

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int h)
    {
        health = h;
    }

    public void damage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
