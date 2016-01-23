using UnityEngine;
using System.Collections;

public class grapple_hook_control : MonoBehaviour
{
    Vector3 last_position;
    private Rigidbody body;
    public GameObject parent_object;
    public GameObject grabbed_object;

    public float max_distance;
    public float speed;
    public bool has_fired;
    public float snap_distance;
    public float hook_distance;
    bool extending;
    bool retracting;

    bool hook_shot;
    bool pull_shot;


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
        else if(Input.GetButton("Fire2"))
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
        float distance = Vector3.Distance(transform.position, transform.parent.position);

        Vector3 initial = transform.position;
        Vector3 move_by = initial;
        Vector3 end_position = initial;

        if (extending == true && distance < max_distance)
        {
            print("Extending");
            move_by = transform.forward * (speed * Time.deltaTime);
            end_position = initial + move_by;
        }
        else if (extending == true && distance >= max_distance)
        {
            print("Switching");
            extending = false;
            retracting = true;
        }
        else if (retracting == true && distance > snap_distance)
        {
            print("Retracting");
            move_by = -transform.forward * (speed * Time.deltaTime);
            end_position = initial + move_by;
        }

        if (pull_shot || extending)
        {
            transform.position = end_position;
        }
        if(retracting && hook_shot)
        {
            transform.position = end_position;
            parent_object.transform.position = parent_object.transform.position - move_by;
        }

        if(retracting == true && hook_shot == true && distance < hook_distance)
        {
            extending = false;
            retracting = false;
            has_fired = false;
        }

        if (retracting == true && distance <= snap_distance)
        {
            extending = false;
            retracting = false;
            has_fired = false;
            for (int i = 0; i < transform.GetChildCount();i++ )
            {
                transform.GetChild(i).transform.parent = null;
                i = 0;
            }
                transform.position = transform.parent.transform.position;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Gun"))
        {
            if (extending == true)
            {
                col.transform.parent = this.transform;
                extending = false;
                retracting = true;
                if(hook_shot == true)
                {
                    last_position = col.gameObject.transform.position;
                }
            }
        }
    }
}