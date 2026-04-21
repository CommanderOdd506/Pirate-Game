using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    [Tooltip("Where the player will actually be placed on respawn.")]
    [SerializeField] private Transform respawnPoint;

    //The world position the player respawns at.
    public Vector3 RespawnPoint => respawnPoint != null ? respawnPoint.position : transform.position;
    public Quaternion RespawnRotation => respawnPoint.rotation;

    private bool _activated;

    private void OnTriggerEnter(Collider other)
    {
        if (_activated) return;
        if (!other.CompareTag("Player")) return;

        _activated = true;
        CheckpointManager.Instance.SetCheckpoint(this);

    }
}