using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip boulderRoll;
    [SerializeField] StasisRigidbody stasisRigidbody;
    private bool colliding;

    void Update()
    {
        if (colliding && !stasisRigidbody.IsStasised)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Island2")
            colliding = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Island2")
            colliding = false;  // you had a minus sign here instead of =
    }
}