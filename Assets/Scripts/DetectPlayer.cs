using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    RaycastHit hit;
    public roamtest enemy;

    void Awake()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<roamtest>();
    }

    void OnTriggerStay(Collider other)
    {      
        if (other.CompareTag("Player")) 
        {
            print("in trigger");
            Transform playerTrans = other.gameObject.transform;
            if(Physics.Raycast(transform.position, playerTrans.position - transform.position, out hit, 100))
            {
                if(hit.transform.CompareTag("Player"))
                {
                    enemy.Chase();
                    print("here"); // do stuff - Detection logic
                }
                else
                {
                    enemy.Roam();
                }
            }
        }
    }
}
