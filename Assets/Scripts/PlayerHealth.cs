using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int health;
    
	void Start () {
        health = 100;
	}
	
    public int getHealth()
    {
        return health;
    }

    public void setHealth(int h)
    {
        health = h;
    }

    public void damage(int d)
    {
        health -= d;
    }
}
