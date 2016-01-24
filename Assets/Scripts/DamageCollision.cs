using UnityEngine;
using System.Collections;

public class DamageCollision : MonoBehaviour {

    RaycastHit hit;
    public PlayerHealth pHealth;

    void Awake()
    {
        pHealth = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerHealth>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("in trigger");
            Transform playerTrans = other.gameObject.transform;
            if (Physics.Raycast(transform.position, playerTrans.position - transform.position, out hit, 100))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    pHealth.damage(1);
                }
            }
        }
    }
}
