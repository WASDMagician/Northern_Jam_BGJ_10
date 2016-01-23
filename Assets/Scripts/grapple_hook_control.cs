using UnityEngine;
using System.Collections;

public class grapple_hook_control : MonoBehaviour {

    public float speed;
    public bool has_fired;
    public float snap_dist;
    public float max_length;
    private Rigidbody body;
    bool extending;
    bool retracting;


    Vector3 start_position;
    Vector3 gun_position;
    CharacterController controller;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        has_fired = false;
        start_position = transform.position;
        gun_position = transform.parent.transform.position;
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(has_fired)
        {
            Move();
        }
	}

    public void Fire()
    {
        has_fired = true;
        start_position = transform.position;
        retracting = false;
        extending = true;
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, transform.parent.transform.position);
        if(distance < max_length && extending)
        {
            body.MovePosition(transform.position + transform.forward * (speed * Time.deltaTime));
        }
        else if(distance > max_length && extending)
        {
            extending = false;
            retracting = true;
        }

        if (retracting && distance > snap_dist)
        {
            body.MovePosition(transform.position - transform.forward * (speed * Time.deltaTime));
        }
        else if(retracting && distance <= snap_dist)
        {
            transform.position = transform.parent.transform.position;
            has_fired = false;
            extending = false;
            retracting = false;
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        extending = false;
        retracting = true;
    }
}
