﻿using UnityEngine;
using System.Collections;

public class quit_game : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		Application.Quit();
	}
}
