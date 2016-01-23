using UnityEngine;
using System.Collections;

public class grapple_hook_control : MonoBehaviour {

    public float speed;
    public bool has_fired;
    float max_length;
    Vector3 start_position;
    Vector3 gun_position;
    CharacterController controller;

	// Use this for initialization
	void Start () {
        has_fired = false;
        max_length = 5;
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
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, start_position) < max_length)
        {
            transform.position = transform.position + transform.forward * 1f;
        }
    }
}
