using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour
{
    RaycastHit hit;

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            print("in trigger");
            Transform playerTrans = other.gameObject.transform;
            if(Physics.Raycast(transform.position, playerTrans.position - transform.position, out hit, 100))
            {
                if(hit.transform.tag == "Player")
                {
                    print("here"); // do stuff.
                }
            }
        }
    }
}
