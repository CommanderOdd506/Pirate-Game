using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShip : MonoBehaviour
{
    [SerializeField] private GameObject mastMesh;
    [SerializeField] private GameObject wheelMesh;
    [SerializeField] private GameObject rudderMesh;

    [SerializeField] private GameObject brokenBoat;
    [SerializeField] private GameObject completeBoat;

    float partCount;
    // Start is called before the first frame update
    void Start()
    {
        partCount = 0;
        LoadShip();
    }

    void LoadShip()
    {
        
        if (GameManager.HasShipPart("Mast"))
        {
            mastMesh.SetActive(true);
            partCount++;
        }

        if (GameManager.HasShipPart("Wheel"))
        {
            wheelMesh.SetActive(true);
            partCount++;
        }

        if (GameManager.HasShipPart("Rudder"))
        {
            rudderMesh.SetActive(true);
            partCount++;
        }

        if (partCount >= 3)
        {
            brokenBoat.SetActive(false);
            completeBoat.SetActive(true);
        }
    }
}
