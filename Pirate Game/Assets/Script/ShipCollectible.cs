using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ScoreManager.Instance.CompleteLevel();

        this.gameObject.SetActive(false);
    }

}
