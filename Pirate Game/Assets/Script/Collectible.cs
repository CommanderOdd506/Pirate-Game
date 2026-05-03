using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string id;
    public int value = 1;
    public string audioName;
    [SerializeField] private ParticleSystem coinVFX;
    [SerializeField] private CapsuleCollider collider;
    [SerializeField] private GameObject mesh;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (audioName != string.Empty)
        {
            SFXManager.instance.AudioPlayOneShot(audioName);
        }

        CollectibleSystem.Instance.Add(id, value);
        mesh?.SetActive(false);
        collider.enabled = false;
        coinVFX?.Play();
    }
}