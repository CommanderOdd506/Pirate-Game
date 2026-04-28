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


    private void Start()
    {
        string targetIsland = GameManager.GetLastIsland();
        TeleportToIsland(targetIsland);
    }

    private void TeleportToIsland(string islandName)
    {
        foreach (SpawnPoint sp in spawnPoints)
        {
            if (sp.islandName == islandName && sp.spawnTransform != null)
            {
                player.position = sp.spawnTransform.position;
                player.rotation = sp.spawnTransform.rotation;
                Debug.Log($"[MapSpawner] Spawned at: {islandName}");
                return;
            }
        }

        // Fallback — no matching spawn found
        Debug.LogWarning($"[MapSpawner] No spawn point found for '{islandName}'. Staying at default position.");
    }
}
