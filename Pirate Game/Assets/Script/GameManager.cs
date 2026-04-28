using UnityEngine;

/// <summary>
/// Central PlayerPrefs helper. No scene references — pure data layer.
/// Call these from anywhere without needing a GameObject reference.
/// </summary>
public static class GameManager
{
    // ---------------------------------------------------------------
    //  KEYS
    // ---------------------------------------------------------------

    // Stores the island name the player just left (e.g. "CannonIsland")
    private const string LAST_ISLAND_KEY = "LastIsland";

    // Each ship part is stored as "ShipPart_<partName>" with value 0 or 1
    private const string SHIP_PART_PREFIX = "ShipPart_";

    // The default spawn point on the map if no island has been visited yet
    public const string DEFAULT_SPAWN = "HubIsland";


    // ---------------------------------------------------------------
    //  LAST ISLAND  (used by MapSpawner to pick spawn point)
    // ---------------------------------------------------------------

    /// <summary>Call this at the exit of every level scene.</summary>
    public static void SetLastIsland(string islandName)
    {
        PlayerPrefs.SetString(LAST_ISLAND_KEY, islandName);
        PlayerPrefs.Save();
        Debug.Log($"[GameManager] LastIsland saved: {islandName}");
    }

    /// <summary>Returns the island name to spawn at, or DEFAULT_SPAWN if none saved.</summary>
    public static string GetLastIsland()
    {
        return PlayerPrefs.GetString(LAST_ISLAND_KEY, DEFAULT_SPAWN);
    }


    // ---------------------------------------------------------------
    //  SHIP PARTS  (collected once per level, permanent)
    // ---------------------------------------------------------------

    /// <summary>Mark a ship part as collected. Call this when the player receives the part.</summary>
    public static void CollectShipPart(string partName)
    {
        string key = SHIP_PART_PREFIX + partName;
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
        Debug.Log($"[GameManager] Ship part collected: {partName}");
    }

    /// <summary>Returns true if the player has already collected this part.</summary>
    public static bool HasShipPart(string partName)
    {
        return PlayerPrefs.GetInt(SHIP_PART_PREFIX + partName, 0) == 1;
    }


    // ---------------------------------------------------------------
    //  DEBUG UTILITY
    // ---------------------------------------------------------------

    /// <summary>Wipes all game data — useful for testing in the editor.</summary>
    public static void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("[GameManager] All PlayerPrefs cleared.");
    }
}