using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    // Fired when the player reaches a new checkpoint.
    // Passes the Checkpoint that was just activated.
    public static event Action<Checkpoint> OnCheckpointReached;

    // Fired when the player respawns at the last checkpoint.
    // Passes the respawn position so listeners can react.
    public static event Action OnPlayerRespawn;
    public static event Action OnPlayerDie;
    public float DeathDelayTime;

    public Checkpoint ActiveCheckpoint { get; private set; }
    public Checkpoint startingCheckpoint;
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] DeathVignette deathVignette;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        if (startingCheckpoint) { SetCheckpoint(startingCheckpoint); }
    }
    /// <summary>
    /// Called by a Checkpoint when the player enters it.
    /// Ignored if it's already the active checkpoint.
    /// </summary>
    public void SetCheckpoint(Checkpoint checkpoint)
    {
        if (checkpoint == ActiveCheckpoint) return;

        ActiveCheckpoint = checkpoint;
        OnCheckpointReached?.Invoke(checkpoint);

        Debug.Log($"[Checkpoint] New checkpoint: {checkpoint.name}");
    }

    /// <summary>
    /// Teleports the player back to the last activated checkpoint.
    /// </summary>
    public void RespawnPlayer()
    {
        if (ActiveCheckpoint == null)
        {
            Debug.LogWarning("[Checkpoint] No active checkpoint to respawn at.");
            return;
        }
        deathVignette?.FadeIn();

        OnPlayerDie?.Invoke();

        StartCoroutine(DeathDelay());
        Debug.Log($"[Checkpoint] Respawned at {ActiveCheckpoint.name}");
    }
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(DeathDelayTime);

        deathVignette?.FadeOut();

        CharacterController cc = player.GetComponent<CharacterController>();

        Vector3 respawnPos = ActiveCheckpoint.RespawnPoint;
        Quaternion respawnRot = ActiveCheckpoint.RespawnRotation;
        if (cc != null)
        {
            cc.enabled = false;
            player.SetPositionAndRotation(respawnPos, respawnRot);
            yield return null;
            cc.enabled = true;
        }


        OnPlayerRespawn?.Invoke();

    }
}