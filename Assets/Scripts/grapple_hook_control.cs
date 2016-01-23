using UnityEngine;
using System.Collections;

public class grapple_hook_control : MonoBehaviour {

    private Rigidbody body;

    public float max_distance;
    public float speed;
    public bool has_fired;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        has_fired = false;
    }

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            has_fired = true;
        }

        if(has_fired)
        {
            Move();
        }
    }

    void Move()
    {

        print(Vector3.Distance(transform.position, transform.parent.position));
        //body.MovePosition(transform.forward * (speed * Time.deltaTime));
        Vector3 trans = transform.parent.transform.position;
        trans += transform.forward * (speed * Time.deltaTime);
        transform.position += trans;
        print(Vector3.Distance(transform.position, transform.parent.position));
    }


}
