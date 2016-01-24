using UnityEngine;
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
        if (Input.GetButtonDown("Fire1"))
        {
            hook_shot = false;
            pull_shot = true;
            Fire();
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            hook_shot = false;
            pull_shot = false;
            grabbed_object = null;
            has_fired = false;
            Reset_Hook_Position();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            hook_shot = true;
            pull_shot = false;
            Fire();
        }
        else if(Input.GetButtonUp("Fire2"))
        {
            hook_shot = false;
            pull_shot = false;
            grabbed_object = null;
            has_fired = false;
            Reset_Hook_Position();
        }

        if (has_fired)
        {
            Move();
        }
    }

    public void Fire()
    {
        if (has_fired == false || retracting == true)
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

        if (distance < snap_distance)
        {
            extending = false;
            retracting = false;
            has_fired = false;
            grabbed_object = null;
            hook_shot = false;
            pull_shot = false;
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
        distance = Vector3.Distance(parent_object.transform.position, hook_pos);
        parent_object.transform.position = Vector3.Lerp(parent_object.transform.position, hook_pos, Time.deltaTime * (speed / 4));
        print(distance);
        if (distance < snap_distance)
        {
            extending = false;
            retracting = false;
            has_fired = false;
            grabbed_object = null;
            hook_shot = false;
            pull_shot = false;
            transform.position = parent_pos.position;
        }

    }

    void Reset_Hook_Position()
    {
        transform.position = transform.parent.transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if(!col.CompareTag("Gun"))
        {
            if(extending)
            {
                if (pull_shot && (col.CompareTag("Pullable") || col.CompareTag("Enemy")) || hook_shot && (col.CompareTag("Hookable")))
                {
                    grabbed_object = col.gameObject;
                    hook_pos = transform.position;
                    extending = false;
                    retracting = true;
                }
            }
        }
    }
}