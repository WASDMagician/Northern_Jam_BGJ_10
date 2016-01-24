using UnityEngine;
using System.Collections;

public class Transparency : MonoBehaviour {

    [Range(0.0f, 1.0f)]
    public float transparency;
    [Range(0.0f, 1.0f)]
    public float r;
    [Range(0.0f, 1.0f)]
    public float g;
    [Range(0.0f, 1.0f)]
    public float b;

    void Update()
    {
        GetComponent<Renderer>().material.color = new Color(r, g, b, transparency);
	}
	

}
