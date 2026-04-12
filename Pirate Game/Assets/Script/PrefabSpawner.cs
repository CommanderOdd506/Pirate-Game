using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawmObj;
    [SerializeField] private bool loopSpawn;
    [SerializeField] private float loopTime = 3.0f;
    private float loopTimer;


    // Update is called once per frame
    void Update()
    {
        if (loopSpawn)
        {
            loopTimer += Time.deltaTime;
            if (loopTimer > loopTime) 
            {
                SpawnObject();
                loopTimer = 0;
            }
        }
    }

    public void SpawnObject()
    {
        if (spawmObj == null) return;

        Instantiate(spawmObj, transform.position, transform.rotation);
    }
}
