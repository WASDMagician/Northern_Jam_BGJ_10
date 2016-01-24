using UnityEngine;
using System.Collections;

public class DestroyPlatform : MonoBehaviour
{
    private Renderer rend;
    private float time;

	void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.green;
    }

    public void OnSpawn(float value)
    {
        time = value;
        Invoke("Degrade", time);
    }

    void Degrade()
    {
        rend.material.color = Color.red;
        Invoke("Destroy", time);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
