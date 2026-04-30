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
    private const string LAST_ISLAND_KEY = "LastIsland";
    private const string SHIP_PART_PREFIX = "ShipPart_";
    private const string HUB_VISITED_KEY = "HubVisited";

    public const string DEFAULT_SPAWN = "HubIsland";

    // All known ship part names — add new parts here as your game grows.
    // ResetGameData uses this list to know exactly what to wipe.
    private static readonly string[] SHIP_PART_NAMES =
    {
         "Mast", "Wheel", "Rudder"
        // Add your actual part names here
    };

    // ---------------------------------------------------------------
    //  LAST ISLAND
    // ---------------------------------------------------------------
    public static void SetLastIsland(string islandName)
    {
        PlayerPrefs.SetString(LAST_ISLAND_KEY, islandName);
        PlayerPrefs.Save();
        Debug.Log($"[GameManager] LastIsland saved: {islandName}");
    }

    public static string GetLastIsland()
    {
        return PlayerPrefs.GetString(LAST_ISLAND_KEY, DEFAULT_SPAWN);
    }

    // ---------------------------------------------------------------
    //  HUB
    // ---------------------------------------------------------------
    public static void VisitedHub()
    {
        PlayerPrefs.SetInt(HUB_VISITED_KEY, 1);
        PlayerPrefs.Save();
    }

    public static bool HasVisitedHub()
    {
        return PlayerPrefs.HasKey(HUB_VISITED_KEY);
    }

    // ---------------------------------------------------------------
    //  SHIP PARTS
    // ---------------------------------------------------------------
    public static void CollectShipPart(string partName)
    {
        PlayerPrefs.SetInt(SHIP_PART_PREFIX + partName, 1);
        PlayerPrefs.Save();
        Debug.Log($"[GameManager] Ship part collected: {partName}");
    }

    public static bool HasShipPart(string partName)
    {
        return PlayerPrefs.GetInt(SHIP_PART_PREFIX + partName, 0) == 1;
    }

    // ---------------------------------------------------------------
    //  DEBUG UTILITIES
    // ---------------------------------------------------------------

    /// <summary>
    /// Wipes only game state data (island progress, ship parts, hub visit).
    /// Settings stored in PlayerPrefs under other keys are left untouched.
    /// </summary>
    public static void ResetGameData()
    {
        PlayerPrefs.DeleteKey(LAST_ISLAND_KEY);
        PlayerPrefs.DeleteKey(HUB_VISITED_KEY);

        foreach (string part in SHIP_PART_NAMES)
            PlayerPrefs.DeleteKey(SHIP_PART_PREFIX + part);

        PlayerPrefs.Save();
        Debug.Log("[GameManager] Game state reset. Settings preserved.");
    }

    /// <summary>
    /// Wipes ALL PlayerPrefs including settings. Use with caution.
    /// </summary>
    public static void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("[GameManager] All PlayerPrefs cleared.");
    }
}