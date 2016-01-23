using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    /// <summary>
    /// Lerps based on the requireSwitchBool.
    /// if on, needs a platformLever to move.
    /// if off, will move endlessly via Mathf.PingPong().
    /// in inspector, lerpspeed controls speed when reqSwitch is off.
    /// </summary>

    [Range(0.0f, 1.0f)]
    public float lerpSpeed;
    public Transform start;
    public Transform end;
    public bool requireSwitch;

    private Transform player;
    private Transform playerParent;
    private float lerpAmount;
    private bool isStationary;


    void Start ()
    {
        isStationary = true;
        lerpAmount = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerParent = player.parent.transform;
    }

    void FixedUpdate()
    {
        switch(requireSwitch)
        {
            case true:
                transform.position = Vector3.Lerp(start.position, end.position, lerpAmount);
                break;
            case false:
                transform.position = Vector3.Lerp(start.position, end.position, Mathf.PingPong(Time.time * lerpSpeed, 1));
                break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(start.position, new Vector3(1, 1, 1));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(end.position, new Vector3(1, 1, 1));
    }

    void OnTriggerEnter(Collider other)
    {
        print("Collided");
        if(other.gameObject.CompareTag("Player"))
        {
            player.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.parent = playerParent;
        }
    }

    public bool IsStationary()
    {
        return isStationary;
    }

    public void MovePlatform()
    {
        isStationary = false;
        if(transform.position == start.position)
        {
            StartCoroutine(OneWayMovement(1));
        }
        else
        {
            StartCoroutine(OneWayMovement(-1));
        }
    }

    IEnumerator OneWayMovement(int val)
    {
        for(int i = 0; i < 50; i++)
        {
            lerpAmount += (0.02f * val);
            yield return new WaitForSeconds(0.02f);
        }
        isStationary = true;
    }
}
