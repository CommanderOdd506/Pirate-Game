using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawmObj;
    [SerializeField] private bool loopSpawn;
    
    [SerializeField] private float loopTime = 3.0f;


    [Header ("Random")]
    [SerializeField] private bool randomSpawn;
    [SerializeField] float minTime = 1.0f;
    [SerializeField] float maxTime = 2.0f;

    private float loopTimer;
    private float randomTime;
    // Update is called once per frame

    void Start()
    {
        randomTime = GetRandomTime();
    }
    void Update()
    {
        if (loopSpawn)
        {
            loopTimer += Time.deltaTime;

            if (randomSpawn && loopTimer > randomTime)
            {
                SpawnObject();
                loopTimer = 0;
                randomTime = GetRandomTime();
            }
            else if (!randomSpawn && loopTimer > loopTime) 
            {
                SpawnObject();
                loopTimer = 0;
            }
        }
    }

    float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }

    public void SpawnObject()
    {
        if (spawmObj == null) return;

        Instantiate(spawmObj, transform.position, transform.rotation);
    }
}
