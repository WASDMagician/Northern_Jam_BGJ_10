using UnityEngine;
using System.Collections;

public class pulse : MonoBehaviour {

    Renderer renderer;
    Material material;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
	    float emission = Mathf.PingPong(Time.time, 1.0f);
        Color base_color = material.color;

        Color final_color = base_color * Mathf.LinearToGammaSpace(emission);

        material.SetColor("_EmissionColor", final_color);
	}
}
