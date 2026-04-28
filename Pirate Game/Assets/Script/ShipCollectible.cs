using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollectible : MonoBehaviour
{
    [SerializeField] private string shipPartName;
    [SerializeField] private string islandName;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.CollectShipPart(shipPartName);

        GameManager.SetLastIsland(islandName);

        ScoreManager.Instance.CompleteLevel();

        this.gameObject.SetActive(false);
    }

}
