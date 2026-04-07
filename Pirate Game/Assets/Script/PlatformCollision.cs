using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform platform;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            other.gameObject.transform.parent = platform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
