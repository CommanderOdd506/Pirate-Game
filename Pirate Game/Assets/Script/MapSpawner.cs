using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        [Tooltip("Must match the islandName used in LevelExit")]
        public string islandName;

        [Tooltip("Where the player spawns when returning from this island")]
        public Transform spawnTransform;
    }

    [Header("Spawn Points — one per island + one for HubIsland default")]
    public SpawnPoint[] spawnPoints;
    public Transform player;
    private CharacterController characterController;


    private void Start()
    {
        string targetIsland = GameManager.GetLastIsland();
        characterController = player.gameObject.GetComponent<CharacterController>();
        TeleportToIsland(targetIsland);
    }

    private void TeleportToIsland(string islandName)
    {
        foreach (SpawnPoint sp in spawnPoints)
        {
            if (sp.islandName == islandName && sp.spawnTransform != null)
            {
                characterController.enabled = false;
                player.position = sp.spawnTransform.position;
                player.rotation = sp.spawnTransform.rotation;
                characterController.enabled = true;
                Debug.Log($"[MapSpawner] Spawned at: {islandName}");
                return;
            }
        }

        // Fallback — no matching spawn found
        Debug.LogWarning($"[MapSpawner] No spawn point found for '{islandName}'. Staying at default position.");
    }
}
