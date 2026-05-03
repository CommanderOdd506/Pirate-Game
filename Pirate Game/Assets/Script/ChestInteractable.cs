using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteract
{
    public Transform[] coinSpots;
    public GameObject coinPrefab;

    public Animator chestAnimator;

    public void SpawnCoins()
    {
        foreach (Transform coin in coinSpots)
        {
            Instantiate(coinPrefab, coin.position, coin.rotation);
        }
    }
    public void OnInteract()
    {
        if (chestAnimator != null)
        {
            chestAnimator.SetTrigger("Open");
            SFXManager.instance.AudioPlayOneShot("Chest",1.4f);
        }
    }
}
