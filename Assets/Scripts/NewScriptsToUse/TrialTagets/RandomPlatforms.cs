using UnityEngine;
using System.Collections;

public class RandomPlatforms : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject prefab;
    [Range(0.0f, 10.0f)]
    public float startTime;
    [Range(0.0f, 10.0f)]
    public float time;

    private int amount;

    void Start ()
    {
        amount = spawnPoint.Length;
        InvokeRepeating("SpawnPlatform", startTime, time);
	}

    void SpawnPlatform()
    {
        int random = Random.Range(0, amount);
        Transform spawn = spawnPoint[random];
        GameObject instance = (GameObject)Instantiate(prefab, spawn.position, Quaternion.identity);
        instance.SendMessage("OnSpawn", time);
        ArrangeArray(random);
        if (amount > spawnPoint.Length - 2)
        {
            amount--;
        }

    }

    void ArrangeArray(int val)
    {
        int value;
        switch(amount)
        {
            case 5:
                value = 2;
                break;
            case 4:
                value = 3;
                break;
            default:
                value = 3;
                break;
        }
        int random = Random.Range(1, value);
        Transform currentSpawn = spawnPoint[val];
        Transform endOfArray = spawnPoint[spawnPoint.Length - random];
        spawnPoint[val] = endOfArray;
        spawnPoint[spawnPoint.Length - random] = currentSpawn;
    }

    public float GetTime()
    {
        return time;
    }
}
