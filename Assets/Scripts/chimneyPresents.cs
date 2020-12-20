using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chimneyPresents : MonoBehaviour
{
    public GameObject present;

    bool doSpawn = false;
    float lastSpawnTime = -10;
    List<GameObject> spawnedPresents;

    private void Start() {
        spawnedPresents = new List<GameObject>();    
    }

    private void FixedUpdate()
    {
        if(doSpawn && spawnedPresents.Count < LEVELDATA.instance.PresentsToSpawn && Time.time > lastSpawnTime + 1f)
        {
            GameObject go = GameObject.Instantiate(present);
            go.transform.position = transform.position;
            lastSpawnTime = Time.time;
            spawnedPresents.Add(go);
        }
    }

    public bool StartSpawn()
    {

        if((spawnedPresents.Count == LEVELDATA.instance.PresentsToSpawn) || (lastSpawnTime == -10))
        {
            spawnedPresents.ForEach(GameObject.Destroy);
            spawnedPresents.Clear();

            doSpawn = true;
            LEVELDATA.instance.CurrentPresents = 0;
            return true;
        }
        return false;
    }
}
