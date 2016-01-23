using UnityEngine;
using System.Collections;

public class grapple_hook_control : MonoBehaviour {

    Vector3 initial_rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Set_Init()
    {
        initial_rotation = transform.rotation.eulerAngles;
    }

    void Fire()
    {

    }
}
