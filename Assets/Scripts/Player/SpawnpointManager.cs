using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointManager : MonoBehaviour
{
    public Spawnpoint[] spawnpoints;

    public Transform defaultSpawn;

    void Start()
    {
        string lastOne = GameManager.instance.save.lastMap;

        bool foundSpawn = false;
        Transform currentPos = PlayerHealth.instance.transform;

        foreach (Spawnpoint spawn in spawnpoints)
        {
            if (spawn.nameLast == lastOne)
            {
                currentPos = spawn.position;
                foundSpawn = true;
                break;
            }
        }

        if (!foundSpawn)
        {
            currentPos = defaultSpawn;
        }

        PlayerHealth.instance.transform.position = currentPos.position;
        PlayerHealth.instance.transform.eulerAngles = currentPos.eulerAngles;

        Destroy(gameObject);
    }

}
