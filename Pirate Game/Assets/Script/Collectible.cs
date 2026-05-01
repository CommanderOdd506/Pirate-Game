using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string id;
    public int value = 1;
    public string audioName;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (audioName != string.Empty)
        {
            SFXManager.instance.AudioPlayOneShot(audioName);
        }

        CollectibleSystem.Instance.Add(id, value);
        this.gameObject.SetActive(false);
    }
}