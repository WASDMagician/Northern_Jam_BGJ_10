using UnityEngine;
using System.Collections;

public class grapple_hook_control : MonoBehaviour
{

    private Rigidbody body;

    public float max_distance;
    public float speed;
    public bool has_fired;
    public float snap_distance;
    bool extending;
    bool retracting;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        has_fired = false;
        extending = false;
        retracting = false;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
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

        transform.position = end_position;

        if (retracting == true && distance <= snap_distance)
        {
            extending = false;
            retracting = false;
            has_fired = false;
            transform.position = transform.parent.transform.position;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Gun"))
        {
            if (extending == true)
            {
                extending = false;
                retracting = true;
            }
        }
    }


}
