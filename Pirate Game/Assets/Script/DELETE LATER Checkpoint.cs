using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointIndex; // 1, 2, or 3
    public DeletelaterFailScript failScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            failScript.SetRespawnPoint(checkpointIndex);
        }
    }
}