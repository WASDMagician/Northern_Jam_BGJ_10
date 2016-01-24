using UnityEngine;
using System.Collections;

public class kill_zone : MonoBehaviour {
    public GameObject spawn;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        col.gameObject.transform.position = GameObject.FindGameObjectWithTag("Spawn_Point").transform.position;
    }
}
