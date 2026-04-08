using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletelaterFailScript : MonoBehaviour
{
    public Transform respawnOne;
    public Transform respawnTwo;
    public Transform respawnThree;

    public static Transform currentRespawn;

    void Awake()
    {
        if (currentRespawn == null)
        {
            currentRespawn = respawnOne;
            Debug.Log("Default respawn set to respawnOne");
        }
    }

    public void SetRespawnPoint(int index)
    {
        if (index == 1) currentRespawn = respawnOne;
        if (index == 2) currentRespawn = respawnTwo;
        if (index == 3) currentRespawn = respawnThree;

        Debug.Log("Respawn updated to: " + currentRespawn.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Respawning player at: " + currentRespawn.name);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                other.transform.position = currentRespawn.position;
                cc.enabled = true;
            }
            else
            {
                other.transform.position = currentRespawn.position;
            }

            other.transform.rotation = currentRespawn.rotation;
        }
    }
}