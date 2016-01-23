using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    public List<Enemy> enemies;

    // Use this for initialization
    void Start () {
	
	}

    void Update()
    {
        //testing the script for now
        for (int i = 0; i < enemies.Count; i++)
        {
            Debug.Log(enemies[i].health);
        }
    }
}
