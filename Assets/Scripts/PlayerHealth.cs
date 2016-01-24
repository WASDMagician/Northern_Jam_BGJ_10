using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int health;
    private Text health_display;
    
	void Start () {
        health = 100;
        health_display = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
        health_display.text = "Health: " + health.ToString();

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
        health_display.text = "Health: " + health.ToString();
    }
}
