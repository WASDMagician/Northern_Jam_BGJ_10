﻿using UnityEngine;
using System.Collections;

public class grapple_hook_control : MonoBehaviour
{
    
    Vector3 last_position;
    private Rigidbody body;
    public GameObject parent_object;
    public GameObject grabbed_object;
    Transform parent_pos;
    float distance;
    Vector3 hook_pos;
    public float max_distance;
    public float speed;
    public bool has_fired;
    public float snap_distance;
    public float hook_distance;
    bool extending;
    bool retracting;

    public bool hook_shot;
    public bool pull_shot;


    void Start()
    {
        body = GetComponent<Rigidbody>();
        has_fired = false;
        extending = false;
        retracting = false;
        hook_shot = false;
        pull_shot = false;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            hook_shot = false;
            pull_shot = true;
            Fire();
        }
        else if (Input.GetButton("Fire2"))
        {
            hook_shot = true;
            pull_shot = false;
            Fire();
        }

        if (has_fired)
        {
            Move();
        }
    }

    public void Fire()
    {
        if (has_fired == false)
        {
            has_fired = true;
            extending = true;
            retracting = false;
        }
    }

    void Move()
    {
        if(extending)
        {
            Extend();
        }
        if(retracting)
        {
            if(grabbed_object != null && pull_shot)
            {
                Pull_Object();
            }
            else if(grabbed_object != null && hook_shot)
            {
                Pull_Player();
            }
            else if(grabbed_object == null)
            {
                Retract();
            }
        }
    }

    void Extend()
    {
        parent_pos = transform.parent.transform;
        distance = Vector3.Distance(transform.position, parent_pos.position);
        transform.position += parent_pos.forward * (speed * Time.deltaTime);

        if(distance > max_distance)
        {
            extending = false;
            retracting = true;
        }
        
    }

    void Retract()
    {
        parent_pos = transform.parent.transform;
        distance = Vector3.Distance(transform.position, parent_pos.position);
        transform.position -= parent_pos.forward * (speed * Time.deltaTime);

        if (distance < hook_distance)
        {
            extending = false;
            retracting = false;
            has_fired = false;
            grabbed_object = null;
            transform.position = parent_pos.position;
        }
    }

    void Pull_Object()
    {
        Retract();
        grabbed_object.transform.position = transform.position;
    }

    void Pull_Player()
    {
        //distance is between player object and grabbed object
        //target is grabbed object
        //move player object toward grabbed object
        //parent_object.transform.position += new Vector3(0, 10, 0);
        parent_object.transform.position += grabbed_object.transform.forward * (speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if(!col.CompareTag("Gun"))
        {
            if(extending)
            {
                grabbed_object = col.gameObject;
                hook_pos = grabbed_object.transform.position;
                extending = false;
                retracting = true;
            }
        }
    }
}