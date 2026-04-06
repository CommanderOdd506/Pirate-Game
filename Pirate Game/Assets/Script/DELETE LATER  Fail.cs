using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletelaterFailScript : MonoBehaviour
{
    public Transform respawn;
    public GameObject player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null )
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                other.transform.position = respawn.position;
                cc.enabled = true;
            }
            else
            {
                other.transform.position = respawn.position;
            }
            other.transform.rotation = respawn.rotation;
        }
    }
}
