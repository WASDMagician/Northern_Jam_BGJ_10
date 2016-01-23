using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int health;
    private bool alerted;

    public Transform target;
    NavMeshAgent nmAgent;
    //private Vector3 startPos; //starting position
   // private float roamSpeed;
   // public float wanderRadius;

    // Use this for initialization
    void Start() {
        alerted = false;
        nmAgent = GetComponent<NavMeshAgent>();
       // startPos = transform.position;
       // roamSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update() {
        Alert();

        nmAgent.SetDestination(target.position);
    }

    public int getHealth()
    {
        return health;
    }

    // Possibly dont need this as we are setting health in hierarchy 
    public void setHealth(int h)
    {
        health = h;
    }

    // Enemy take damage
    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public bool getAlerted()
    {
        return alerted;
    }

    public void setAlerted(bool alert)
    {
        alerted = alert;
    }

    // Alert other guards
    public void Alert()
    {
        //alerted = true;
        // some more logic
        if(getAlerted() == true)
        {
            Debug.Log("Alerting AI...");
            //do stuff
        }
    }
}
