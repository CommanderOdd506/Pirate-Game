using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip boulderRoll;

    void Update()
    {
        if(this.isStasised == true)
        {
            Debug.Log("Cool");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Island2")
        {
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Island2")
        {
            audioSource.Stop();
        }
    }
}
