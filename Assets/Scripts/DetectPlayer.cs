using UnityEngine;
using System.Collections.Generic;

public class DetectPlayer : MonoBehaviour
{
    RaycastHit hit;
    public roamtest enemy;

    void Awake()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<roamtest>();
        enemy = this.gameObject.GetComponentInParent<roamtest>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("in trigger");
            Transform playerTrans = other.gameObject.transform;
            if (Physics.Raycast(transform.position, playerTrans.position - transform.position, out hit, 100))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    // enemy.Chase();
                    enemy.isAtPosition = false;
                    enemy.playerSpotted = true;

                }
                else
                {
                    //enemy.Roam();
                    enemy.isAtPosition = false;
                    enemy.playerSpotted = false;

                }

            }
        }
    }
}
