using UnityEngine;

public class WSTextLookAt : MonoBehaviour 
{ 
    /// <summary>
    ///  Makes a world canvas look at the player.
    ///  Euler angles were used to lock the x and z components.
    /// </summary>
    private Transform player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	void Update ()
    {
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
